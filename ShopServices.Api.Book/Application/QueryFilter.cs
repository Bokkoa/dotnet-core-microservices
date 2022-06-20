using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopServices.Api.Book.Model;
using ShopServices.Api.Book.Persistence;

namespace ShopServices.Api.Book.Application
{
    public class QueryFilter
    {
        public class UniqueBook: IRequest<BookMaterialDto>{
            public Guid? BookId { get; set; }
        }

        public class Handler : IRequestHandler<UniqueBook, BookMaterialDto>
        {
            private readonly IMapper _mapper;
            private readonly BookDbContext _context;

            public Handler(IMapper mapper, BookDbContext context )
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<BookMaterialDto> Handle(UniqueBook request, CancellationToken cancellationToken)
            {
                var book  = await _context.BookMaterial.Where( x => x.BookMaterialId == request.BookId ).FirstOrDefaultAsync();

                if( book == null ) throw new Exception("Book not found");

                var bookDto = _mapper.Map<BookMaterial, BookMaterialDto>(book);

                return bookDto;
            }
        }
    }
}