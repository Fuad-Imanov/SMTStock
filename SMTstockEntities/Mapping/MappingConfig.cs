using AutoMapper;
using SMTstock.Entities.DTO.CategoryDto;
using SMTstock.Entities.DTO.MerchantDto;
using SMTstock.Entities.DTO.OrderDto;
using SMTstock.Entities.DTO.OrderProductDto;
using SMTstock.Entities.DTO.ProductDto;
using SMTstock.Entities.Models;

namespace SMTstock.Entities.Mapping
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {

            //map AddProductDTO to Product
            CreateMap<AddProductDTO, Product>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.Balance))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId)).ReverseMap();

            //map ProductDTO to Product
            CreateMap<ProductDTO, Product>().ReverseMap();

            // Define the mapping from Order to OrderGetDTO
            CreateMap<Order, OrderGetDTO>()
                .ForMember(dest => dest.OrdersProducts, opt => opt.MapFrom(src => src.OrdersProducts));

            // Define the mapping from OrderProduct to OrderProductGetDTO
            CreateMap<OrderProduct, OrderProductGetDTO>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.Id))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Product.Price))
                .ForMember(dest => dest.ProductDTO, opt => opt.MapFrom(src => new ProductDTO
                {
                    Id = src.Product.Id,
                    ProductName = src.Product.ProductName,
                    Price = src.Product.Price,
                    Balance = src.Product.Balance,
                    CategoryId = src.Product.CategoryId,
                }));

            //map Order to OrderUpdateDTO
            CreateMap<Order, OrderUpdateDTO>();
            CreateMap<OrderUpdateDTO, Order>()
            .ForMember(dest => dest.OrdersProducts, opt => opt.MapFrom(src => src.OrdersProducts));

            //map OrderProductUpdateDTO to OrderProduct
            CreateMap<OrderProductUpdateDTO, OrderProduct>().ReverseMap();

            CreateMap<OrderUpdateDTO, OrderCreateDTO>()
            .ForMember(dest => dest.OrdersProducts, opt => opt.MapFrom(src => src.OrdersProducts));

            //map Category to CategoryDTO
            CreateMap<CategoryDTO, Category>().ReverseMap();

            //map AddCategoryDTO to Category
            CreateMap<AddCategoryDTO, Category>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryName));

            //map from MerchantDTO to Merchant
            CreateMap<MerchantDTO, Merchant>().ReverseMap();

            //map from AddMerchantDTO to MerchantDTO
            CreateMap<AddMerchantDTO, Merchant>()
            .ForMember(desc => desc.MerchantName, opt => opt.MapFrom(src => src.MerchantName));
        }

    }
}
