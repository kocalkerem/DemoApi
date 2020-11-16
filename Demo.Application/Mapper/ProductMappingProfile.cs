using AutoMapper;
using Demo.Application.Models;
using Demo.Core.Entities;

namespace Demo.Application.Mapper
{
    public class ProductMappingProfile:Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductModel>()
                 .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category))
                 .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.Price))
                 .ReverseMap(); 
        }
    }
}
