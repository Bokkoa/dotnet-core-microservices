using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShopServices.Api.Cart.Model;
using ShopServices.Api.Cart.Persistence;

namespace ShopServices.Api.Cart.Application
{
    public class New
    {
        public class Execute: IRequest {
            public DateTime CreationDate { get; set; }
            public List<string> ProductList { get; set; }
        }

        public class Handler : IRequestHandler<Execute>
        {
            private readonly CartDbContext _context;

            public Handler(CartDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                var cartSession = new CartSession{
                    CreationDate = request.CreationDate
                };

                _context.CartSession.Add(cartSession);
                var rowsAffected = await _context.SaveChangesAsync();

                if( rowsAffected == 0) throw new Exception("Error writing the cart row");

                int id = cartSession.CartSessionId;

                foreach( var obj in request.ProductList ){
                    var detailSession = new CartSessionDetail{
                        CreationDate = DateTime.Now,
                        CartSessionId = id,
                        BookSelected = obj
                    };

                    _context.CartSessionDetail.Add(detailSession);
                }

                rowsAffected = await _context.SaveChangesAsync();

                if( rowsAffected > 0 ){
                    return Unit.Value;
                }

                throw new Exception("Error inserting the cart data");

            }
        }
    }
}