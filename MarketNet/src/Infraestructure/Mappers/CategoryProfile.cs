using AutoMapper;
using MarketNet.Application.Categories.Dto;
using MarketNet.Domain.Entities.Products;

namespace MarketNet.Infraestructure.Mappers
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryBriefDto>()
                .ReverseMap();
            CreateMap<Category, CategoryChildDto>()
                .ReverseMap()
                .AfterMap((src, dest, ctx) =>
                    {
                        var parent = ctx.Items["parent"] as Category;
                        dest.ParentCategory = parent;
                        dest.ParentCategoryId = parent?.Id;
                    })
                .ForMember(d => d.Id, o =>
                {
                    o.PreCondition(src => src.Id > 0);
                    o.NullSubstitute(null);
                });

            CreateMap<Category, CategoryBriefDto>();

            CreateMap<Category, CategoryDto>()
                .ForMember(d => d.ParentCategory, o => o.MapFrom(s => s.ParentCategory))
                .ForMember(d => d.ChildCategories, o => o.MapFrom(s => s.ChildCategories))
                .ReverseMap();
        }
    }
}
