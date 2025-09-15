using MarketNet.Application.Categories.Commands;
using MarketNet.Application.Categories.Dto;
using Swashbuckle.AspNetCore.Filters;

namespace MarketNet.src.WebApi.Swagger.Examples.Categories
{

    public class CreateCategoryCommandExample : IExamplesProvider<CreateCategoryCommand>
    {
        public CreateCategoryCommand GetExamples() => new CreateCategoryCommand
        {
            Name = "Electrónica",
            Slug = "electronica",
            Description = "Dispositivos y gadgets",
            ParentCategoryId = null
        };
    }

    public class CreateCategoryCommandWithParentExample : IExamplesProvider<CreateCategoryCommand>
    {
        public CreateCategoryCommand GetExamples() => new CreateCategoryCommand
        {
            Name = "Smartphones",
            Slug = "smartphones",
            Description = "Teléfonos inteligentes",
            ParentCategoryId = 1,
            ParentCategory = new CategoryBriefDto { Id = 1, Name = "Electrónica", Slug = "electronica" }
        };
    }

    public class CreateCategoryCommandWithChildrenExample : IExamplesProvider<CreateCategoryCommand>
    {
        public CreateCategoryCommand GetExamples() => new CreateCategoryCommand
        {
            Name = "Moda",
            Slug = "moda",
            Description = "Ropa y accesorios",
            ChildCategories = new List<CategoryChildDto>
        {
            new CategoryChildDto
            {
                Id = 10,
                Name = "Hombre",
                Slug = "moda-hombre"
            },
            new CategoryChildDto
            {
                Id = 11,
                Name = "Mujer",
                Slug = "moda-mujer"
            }
        }
        };
    }


}
