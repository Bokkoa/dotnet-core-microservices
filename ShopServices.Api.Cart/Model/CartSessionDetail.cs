using System;

namespace ShopServices.Api.Cart.Model
{
    public class CartSessionDetail
    {
        public int CartSessionDetailId { get; set; }
        public DateTime? CreationDate { get; set; }
        public string BookSelected { get; set; }
        public int CartSessionId { get; set; }

        public CartSession CartSession { get; set; }
    }
}