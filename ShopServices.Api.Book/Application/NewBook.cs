using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Shopservices.RabbitMQ.Bus.BusRabbit;
using Shopservices.RabbitMQ.Bus.EventQueue;
using ShopServices.Api.Book.Model;
using ShopServices.Api.Book.Persistence;

namespace ShopServices.Api.Book.Application
{
    public class NewBook
    {
        public class Execute: IRequest{
            public string Title { get; set; }
            public DateTime PublishDate { get; set; }
            public Guid AuthorBook { get; set; }
        }

        public class ExecuteValidation: AbstractValidator<Execute>{

            public ExecuteValidation(){
                RuleFor( x => x.Title ).NotEmpty();
                RuleFor( x => x.PublishDate ).NotEmpty();
                RuleFor( x => x.AuthorBook ).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Execute>
        {

            private readonly BookDbContext _context;
            private readonly IRabbitEventBus _rabbitEventBus;

            public Handler(BookDbContext context, IRabbitEventBus rabbitEventBus)
            {
                _context = context;
                _rabbitEventBus = rabbitEventBus;
            }

            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                var bookMaterial = new BookMaterial{
                    Title = request.Title,
                    PublishDate = request.PublishDate,
                    AuthorBook = request.AuthorBook,
                };

                _context.BookMaterial.Add(bookMaterial);
                var rowsAffected = await _context.SaveChangesAsync();

                if( rowsAffected > 0 ) return Unit.Value;

                _rabbitEventBus.Publish(new EmailEventQueue("kone.jo@example.com", request.Title, "This is an example"));
                
                throw new Exception("Error happened when try to insert book");
            }
        }
    }
}