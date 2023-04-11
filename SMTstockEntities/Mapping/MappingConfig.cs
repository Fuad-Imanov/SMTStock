using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SMTstock.Entities.DTO.OrderDto;
using SMTstock.Entities.DTO.ProductDto;
using SMTstock.Entities.Models;

namespace SMTstock.Entities.Mapping
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<AddProductDto, Product>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.Balance))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId)).ReverseMap();


            CreateMap<ProductDTO, Product>().ReverseMap();

           


        }

    }
}
