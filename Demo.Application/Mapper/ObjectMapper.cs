using AutoMapper;
using Demo.Application.Models;
using Demo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Application.Mapper
{
    public class ObjectMapper
    {
        public static IMapper Mapper
        {
            get
            {
                return AutoMapper.Mapper.Instance;
            }
        }
        static ObjectMapper()
        {
            CreateMap();
        }

        private static void CreateMap()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Product, ProductModel>()
                    .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category)) 
                    .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.Price)) 
                    .ReverseMap(); 
            });
        }
    }
}
