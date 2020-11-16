using AutoMapper;
using Demo.Application.Models;
using Demo.Core.Entities;

namespace Demo.API.Mapping
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<Product, ProductModel>().ReverseMap();
        }
    }
}
