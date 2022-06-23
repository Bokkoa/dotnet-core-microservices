using AutoMapper;
using ShopServices.Api.Book.Application;
using ShopServices.Api.Book.Model;

namespace ShopServices.Api.Book.Tests
{
    public class MappingTest: Profile
    {
        public MappingTest(){
            CreateMap<BookMaterial, BookMaterialDto>();
        }
    }
}