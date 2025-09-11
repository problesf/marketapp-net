using AutoMapper;
using MarketNet.src.Application.Products.Dto;
using MarketNet.src.Domain.Entities.Products;
namespace MarketNet.src.Infraestructure.Mappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
        }
    }
}
