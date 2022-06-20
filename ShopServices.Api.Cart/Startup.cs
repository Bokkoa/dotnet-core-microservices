using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ShopServices.Api.Cart.Application;
using ShopServices.Api.Cart.Persistence;
using ShopServices.Api.Cart.RemoteInterfaces;
using ShopServices.Api.Cart.RemoteServices;

namespace ShopServices.Api.Cart
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

            // register the interface
            services.AddScoped<IBookService, BookService>();


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShopServices.Api.Cart", Version = "v1" });
            });

            // mysql
            services.AddDbContext<CartDbContext>(options => {
                options.UseMySQL(Configuration.GetConnectionString("ConnectionDatabase"));
            });

            // MEDIATR
            services.AddMediatR(typeof(New.Handler).Assembly);
            

            // Adding service
            services.AddHttpClient("Books", config => {
                    config.BaseAddress = new Uri(Configuration["Services:Books"]);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShopServices.Api.Cart v1"));
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
