using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
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

            public Handler( BookDbContext context){
                _context = context;
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
                
                throw new Exception("Error happened when try to insert book");
            }
        }
    }
}