using AutoMapper;
using MarketNet.Application.Categories.Dto;
using MarketNet.Domain.Entities.Products;

namespace MarketNet.Infraestructure.Mappers
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryBriefDto>();
            CreateMap<Category, CategoryChildDto>();

            CreateMap<Category, CategoryDto>()
                .ForMember(d => d.ParentCategory,
                    o => o.MapFrom(s => s.ParentCategory))
                .ForMember(d => d.ChildCategories,
                    o => o.MapFrom(s => s.ChildCategories));


        }
    }
}
