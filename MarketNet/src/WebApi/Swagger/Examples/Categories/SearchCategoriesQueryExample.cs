using MarketNet.Application.Categories.Queries;
using Swashbuckle.AspNetCore.Filters;

namespace MarketNet.WebApi.Swagger.Examples.Categories;

public class SearchCategoriesQueryExample : IExamplesProvider<SearchCategoriesQuery>
{
    public SearchCategoriesQuery GetExamples() => new SearchCategoriesQuery
    {
        Name = "Electrónica",
        Slug = "electronica",
        Description = "Gadgets y dispositivos",
        ParentCategoryId = null
    };
}
