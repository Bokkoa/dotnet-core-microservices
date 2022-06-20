using System;

namespace ShopServices.Api.Cart.Application
{
    public class CartDetailDto
    {
        public Guid? BookId { get; set; }
        public string BookTitle { get; set; }

        public string AuthorBook { get; set;}

        public DateTime PublishDate { get; set; }
    }
}