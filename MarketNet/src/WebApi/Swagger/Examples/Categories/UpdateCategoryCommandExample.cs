using MarketNet.Application.Categories.Commands;
using MarketNet.Application.Categories.Dto;
using Swashbuckle.AspNetCore.Filters;

namespace MarketNet.src.WebApi.Swagger.Examples.Categories
{
    public class UpdateCategoryCommandBasicExample : IExamplesProvider<UpdateCategoryCommand>
    {
        public UpdateCategoryCommand GetExamples() => new UpdateCategoryCommand
        {
            Id = 1,
            Name = "Electrónica y gadgets",
            Description = "Dispositivos, gadgets y accesorios"
        };
    }

    public class UpdateCategoryCommandChangeParentExample : IExamplesProvider<UpdateCategoryCommand>
    {
        public UpdateCategoryCommand GetExamples() => new UpdateCategoryCommand
        {
            Id = 2,
            Name = "Auriculares",
            Description = "Auriculares inalámbricos y con cable",
            ParentCategoryId = 1,
            ParentCategory = new CategoryBriefDto
            {
                Id = 1
            }
        };
    }

    public class UpdateCategoryCommandWithChildrenExample : IExamplesProvider<UpdateCategoryCommand>
    {
        public UpdateCategoryCommand GetExamples() => new UpdateCategoryCommand
        {
            Id = 3,
            Name = "Ropa Deportiva",
            Description = "Categoría de ropa deportiva",
            ChildCategories = new List<CategoryChildDto>
        {
            new CategoryChildDto { Id = 12},
            new CategoryChildDto { Id = 13}
        }
        };
    }

}
