using AutoMapper;
using ShopServices.Api.Book.Model;

namespace ShopServices.Api.Book.Application
{
    public class MappingProfile: Profile
    {
        public MappingProfile(){
            CreateMap<BookMaterial, BookMaterialDto>();
        }
    }
}