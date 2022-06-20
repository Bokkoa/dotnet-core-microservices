using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShopServices.Api.Cart.Application;

namespace ShopServices.Api.Cart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController: ControllerBase
    {
        private readonly IMediator _mediator;

        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create( New.Execute data){
            return await _mediator.Send(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CartDto>> GetActionResultAsync( int id ){
            return await _mediator.Send( new Query.Execute{ CartSessionId = id });
        }
    }
}