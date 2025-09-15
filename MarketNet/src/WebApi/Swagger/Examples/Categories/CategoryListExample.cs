using MarketNet.Application.Categories.Dto;
using Swashbuckle.AspNetCore.Filters;

namespace MarketNet.WebApi.Swagger.Examples.Categories;

public class CategoryListExample : IExamplesProvider<IEnumerable<CategoryDto>>
{
    public IEnumerable<CategoryDto> GetExamples() => new[]
    {
        new CategoryDto
        {
            Id = 1,
            Name = "Electrónica",
            Slug = "electronica",
            Description = "Categoría de dispositivos electrónicos",
            ChildCategories = new List<CategoryChildDto>
            {
                new CategoryChildDto { Id = 2, Name = "Smartphones", Slug = "smartphones" }
            }
        },
        new CategoryDto
        {
            Id = 10,
            Name = "Hogar",
            Slug = "hogar",
            Description = "Artículos y accesorios para el hogar"
        }
    };
}
