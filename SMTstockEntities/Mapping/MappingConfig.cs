using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SMTstock.Entities.DTO.CategoryDto;
using SMTstock.Entities.DTO.OrderDto;
using SMTstock.Entities.DTO.OrderProduct;
using SMTstock.Entities.DTO.ProductDto;
using SMTstock.Entities.Models;

namespace SMTstock.Entities.Mapping
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {

            //map AddProductDto to Product
            CreateMap<AddProductDto, Product>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.Balance))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId)).ReverseMap();

            //map ProductDTO to Product
            CreateMap<ProductDTO, Product>().ReverseMap();

            // Define the mapping from Order to OrderGetDto
            CreateMap<Order, OrderGetDto>()
                .ForMember(dest => dest.OrdersProducts, opt => opt.MapFrom(src => src.OrdersProducts));

            // Define the mapping from OrderProduct to OrderProductGetDto
            CreateMap<OrderProduct, OrderProductGetDto>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.Id))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Product.Price))
                .ForMember(dest => dest.ProductDto, opt => opt.MapFrom(src => new ProductDTO
                {
                    Id = src.Product.Id,
                    ProductName = src.Product.ProductName,
                    Price = src.Product.Price,
                    Balance = src.Product.Balance,
                    CategoryId = src.Product.CategoryId,
                }));

            //map Order to OrderUpdateDto
            CreateMap<Order, OrderUpdateDto>();
            CreateMap<OrderUpdateDto, Order>()
            .ForMember(dest => dest.OrdersProducts, opt => opt.MapFrom(src => src.OrdersProducts));

            //map OrderProductUpdateDto to OrderProduct
            CreateMap<OrderProductUpdateDto, OrderProduct>().ReverseMap();

            CreateMap<OrderUpdateDto, OrderCreateDto>()
            .ForMember(dest => dest.OrdersProducts, opt => opt.MapFrom(src => src.OrdersProducts));

            //map Category to CategoryDto
            CreateMap<CategoryDTO, Category>().ReverseMap();

            //map AddCategoryDTO to Category
            CreateMap<AddCategoryDTO, Category>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryName));
        }

    }
}
