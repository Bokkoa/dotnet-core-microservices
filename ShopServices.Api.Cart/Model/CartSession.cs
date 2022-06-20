using System;
using System.Collections.Generic;

namespace ShopServices.Api.Cart.Model
{
    public class CartSession
    {
        public int CartSessionId { get; set; }
        public DateTime? CreationDate { get; set; }
        public ICollection<CartSessionDetail> ListDetail { get; set; }
    }
}