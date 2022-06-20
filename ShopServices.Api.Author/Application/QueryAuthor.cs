using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopServices.Api.Author.Model;
using ShopServices.Api.Author.Persistence;

namespace ShopServices.Api.Author.Application
{
    public class QueryAuthor
    {
        public class AuthorList: IRequest<List<AuthorDto>>{
            
        }

        public class Handler : IRequestHandler<AuthorList, List<AuthorDto>>
        {

            private readonly AuthorDbContext _context;
            private readonly IMapper _mapper;

            public Handler(AuthorDbContext context, IMapper mapper){
                _mapper = mapper;
                _context = context;
            }
            public async Task<List<AuthorDto>> Handle(AuthorList request, CancellationToken cancellationToken)
            {
                var authors = await _context.AuthorBook.ToListAsync();
                var authorsDto = _mapper.Map<List<AuthorBook>, List<AuthorDto>>(authors);
                return authorsDto;
            }
        }
    }
}