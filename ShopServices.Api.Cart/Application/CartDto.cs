using System;
using System.Collections.Generic;

namespace ShopServices.Api.Cart.Application
{
    public class CartDto
    {
        public int CartId { get; set; }
        public DateTime CreationDate { get; set; }

        public List<CartDetailDto> ProductList { get; set; }
    }
}