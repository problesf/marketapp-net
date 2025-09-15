using MarketNet.Application.Categories.Dto;
using Swashbuckle.AspNetCore.Filters;

namespace MarketNet.WebApi.Swagger.Examples.Categories;

public class CategoryDtoExample : IExamplesProvider<CategoryDto>
{
    public CategoryDto GetExamples() => new CategoryDto
    {
        Id = 1,
        Name = "Electrónica",
        Slug = "electronica",
        Description = "Categoría de productos electrónicos",
        ParentCategoryId = null,
        ChildCategories = new List<CategoryChildDto>
        {
            new CategoryChildDto { Id = 2, Name = "Smartphones", Slug = "smartphones" },
            new CategoryChildDto { Id = 3, Name = "Tablets", Slug = "tablets" }
        }
    };
}
