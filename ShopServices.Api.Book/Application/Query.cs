using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopServices.Api.Book.Model;
using ShopServices.Api.Book.Persistence;

namespace ShopServices.Api.Book.Application
{
    public class Query
    {
        public class Execute: IRequest<List<BookMaterialDto>>{

        }

        public class Handler : IRequestHandler<Execute, List<BookMaterialDto>>
        {
            private readonly IMapper _mapper;
            private readonly BookDbContext _context;

            public Handler(IMapper mapper, BookDbContext context)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<List<BookMaterialDto>> Handle(Execute request, CancellationToken cancellationToken)
            {
                var books = await _context.BookMaterial.ToListAsync();

                var booksList = _mapper.Map<List<BookMaterial>, List<BookMaterialDto>>(books);
                
                return booksList;
            }
        }
    }
}