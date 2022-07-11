using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Shopservices.RabbitMQ.Bus.BusRabbit;
using Shopservices.RabbitMQ.Bus.EventQueue;
using Shopservices.RabbitMQ.Bus.Implement;
using ShopServices.Api.Author.Application;
using ShopServices.Api.Author.Persistence;
using ShopServices.Api.Author.RabbitHandler;
using ShopServices.Messenger.Email.SendGridLibrary.Implement;
using ShopServices.Messenger.Email.SendGridLibrary.Interface;

namespace ShopServices.Api.Author
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            // setting the postgres context
            services.AddDbContext<AuthorDbContext>( options => {
                options.UseNpgsql(Configuration.GetConnectionString("ConnectionDatabase"));
            });

            // normal controllers
            // services.AddControllers();

            // appending fluent to the command "New Author"
            // with this appending all class that uses the AbstractValidator
            // class, must be implemented by fluent, dont need to be instanced or configure by here
            // (as the mediatR one)
            services.AddControllers()
                    .AddFluentValidation( 
                        cfg => cfg.RegisterValidatorsFromAssemblyContaining<NewAuthor>());


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShopServices.Api.Author", Version = "v1" });
            });

            // configure mediatr, not needed for each command query handler, 
            // with this instance, all extended class from mediatR  ( as IRequest & handler implemented )
            // the system recognizes them
            services.AddMediatR(typeof(NewAuthor.Handler).Assembly);

            //  auto mapper
            // this also trigger all classes with imapper implementation
            services.AddAutoMapper( typeof(QueryAuthor.Handler ));

            // the rabbitmq
            services.AddSingleton<IRabbitEventBus, RabbitEventBus>( sp => {
                var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();

                return new RabbitEventBus(sp.GetService<IMediator>(), scopeFactory);
            });
            
            services.AddTransient<EmailEventHandler>();

            // registering the rabbitmq consumer
            services.AddTransient<IEventHandler<EmailEventQueue>, EmailEventHandler>();

            // Sendgrid client
            services.AddSingleton<ISendGridSend, SendGridSend>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShopServices.Api.Author v1"));
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            // event configuration
            var eventBus = app.ApplicationServices.GetRequiredService<IRabbitEventBus>();

            eventBus.Subscribe<EmailEventQueue, EmailEventHandler>();
        }
    }
}
