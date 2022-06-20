using System;

namespace ShopServices.Api.Cart.RemoteModel
{
    public class BookRemote
    {
        public Guid? BookMaterialId { get; set; }
        public string Title {get; set; }
        public DateTime? PublishDate { get; set; }
        public Guid? AuthorBook { get; set; }
    }
}