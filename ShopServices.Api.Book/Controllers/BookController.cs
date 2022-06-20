using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShopServices.Api.Book.Application;

namespace ShopServices.Api.Book.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController: ControllerBase
    {

        private readonly IMediator _mediator;

        public BookController(IMediator mediator){
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(NewBook.Execute data){
            return await _mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<List<BookMaterialDto>>> GetBooks(){
            return await _mediator.Send( new Query.Execute());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookMaterialDto>> GetBook( Guid id){
            return await _mediator.Send( new QueryFilter.UniqueBook{
                BookId = id
            });
        }
    }
}