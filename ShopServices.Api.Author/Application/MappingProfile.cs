using System.Collections.Generic;
using AutoMapper;
using ShopServices.Api.Author.Model;

namespace ShopServices.Api.Author.Application
{
    public class MappingProfile: Profile
    {
        public MappingProfile(){
            CreateMap<AuthorBook, AuthorDto>();
        }
    }
}