using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShopServices.Api.Author.Application;
using ShopServices.Api.Author.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopServices.Api.Author.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController: ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorController(IMediator mediator){
            _mediator =  mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> CreateAuthor(NewAuthor.Execute data){
            return await _mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<List<AuthorDto>>> GetAuthors(){
            return await _mediator.Send( new QueryAuthor.AuthorList() );
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> GetAuthor( string id){
            return await _mediator.Send( 
                            new QueryFilterAuthor.AuthorUnique{ 
                                    AuthorGuid = id 
                            });
        }
        
    }
}