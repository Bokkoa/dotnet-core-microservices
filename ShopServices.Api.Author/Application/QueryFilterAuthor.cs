using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopServices.Api.Author.Model;
using ShopServices.Api.Author.Persistence;

namespace ShopServices.Api.Author.Application
{
    public class QueryFilterAuthor
    {
        public class AuthorUnique: IRequest<AuthorDto>{
            public string AuthorGuid  { get; set; }

        }

        public class Handler : IRequestHandler<AuthorUnique, AuthorDto>
        {
            private readonly AuthorDbContext _context;
            private readonly IMapper _mapper;

            public Handler(AuthorDbContext context, IMapper mapper){
                _context = context;
                _mapper = mapper;
            }
            public async Task<AuthorDto> Handle(AuthorUnique request, CancellationToken cancellationToken)
            {
                var author = await _context.AuthorBook.Where(x => x.AuthorBookGuid == request.AuthorGuid ).FirstOrDefaultAsync();

                if( author == null ) throw new Exception("Author not found.");

                var authorDto = _mapper.Map<AuthorBook, AuthorDto>(author);
                return authorDto;
            }
        }
    }
}