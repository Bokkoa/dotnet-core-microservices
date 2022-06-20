using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopServices.Api.Cart.Persistence;
using ShopServices.Api.Cart.RemoteInterfaces;

namespace ShopServices.Api.Cart.Application
{
    public class Query
    {
        public class Execute :IRequest<CartDto>{
            public int CartSessionId { get; set; }
        }

        public class Handler : IRequestHandler<Execute, CartDto>
        {

            private readonly CartDbContext _context;
            private readonly IBookService _bookService;

            public Handler(CartDbContext context, IBookService bookService)
            {
                _context = context;
                _bookService = bookService;
            }

            public async Task<CartDto> Handle(Execute request, CancellationToken cancellationToken)
            {
                var cartSession = await _context.CartSession.FirstOrDefaultAsync( x => x.CartSessionId == request.CartSessionId);

                var cartSessionDetail = await _context.CartSessionDetail.Where(x => x.CartSessionId == request.CartSessionId ).ToListAsync();

                
                // Fetching book details and names 
                var cartListDto = new List<CartDetailDto>();

                foreach( var book in cartSessionDetail){

                        var response = await _bookService.GetBook(new Guid(book.BookSelected));

                        if( response.result ){
                            var bookObject = response.book;
                            var cartDetail = new CartDetailDto{
                                BookTitle = bookObject.Title,
                                PublishDate = bookObject.PublishDate ?? DateTime.Now,
                                BookId = bookObject.BookMaterialId
                            };

                            cartListDto.Add( cartDetail );
                        }

                }


                var cartSessionDto = new CartDto{
                    CartId = cartSession.CartSessionId,
                    CreationDate = cartSession.CreationDate ?? DateTime.Now,
                };

                return cartSessionDto;
            }
        }
    }
}