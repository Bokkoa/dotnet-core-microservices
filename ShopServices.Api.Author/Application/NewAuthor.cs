using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using ShopServices.Api.Author.Model;
using ShopServices.Api.Author.Persistence;

namespace ShopServices.Api.Author.Application
{
    public class NewAuthor
    {
        public class Execute: IRequest{
            public string Name { get; set; }
            public string LastName { get; set; }

            public DateTime? BirthDate { get; set; }
        }

        public class ExecuteValidation: AbstractValidator<Execute>{
            public ExecuteValidation(){
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.LastName).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Execute>
        {
            public readonly AuthorDbContext _context;

            // DIY
            public Handler(AuthorDbContext context ){
                _context = context;
            }

            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                var authorBook = new AuthorBook{
                    Name = request.Name,
                    BirthDate = request.BirthDate,
                    LastName = request.LastName,
                    AuthorBookGuid = Convert.ToString(Guid.NewGuid())
                };

                // adding to context
                _context.AuthorBook.Add(authorBook);

                // store in db
                var affectedRows = await _context.SaveChangesAsync();

                // return ok
                if( affectedRows > 0 ) return Unit.Value;

                throw new Exception("It can insert the authorbook row");
            }
        }
    }
}