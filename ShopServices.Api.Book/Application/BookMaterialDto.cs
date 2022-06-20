using System;

namespace ShopServices.Api.Book.Application
{
    public class BookMaterialDto
    {
        public Guid? BookMaterialId { get; set; }
        public string Title {get; set; }
        public DateTime? PublishDate { get; set; }
        public Guid? AuthorBook { get; set; }
    }
}