using AutoMapper;
using MarketApp.src.Domain.entities.product;
using MarketNet.src.Application.Products.Dto;
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
