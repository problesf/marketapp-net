using AutoMapper;
using MarketNet.Application.Products.Dto;
using MarketNet.Domain.Entities.Products;
namespace MarketNet.Infraestructure.Mappers
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
