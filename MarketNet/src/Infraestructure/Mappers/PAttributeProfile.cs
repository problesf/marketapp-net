using AutoMapper;
using MarketNet.Domain.Entities.Products;
using MarketNet.src.Application.Products.Dto;

namespace MarketNet.src.Infraestructure.Mappers
{
    public class PAttributeProfile : Profile
    {
        public PAttributeProfile()
        {
            CreateMap<PAttribute, PAttributeDto>();
            CreateMap<PAttributeDto, PAttribute>();
        }
    }
}
