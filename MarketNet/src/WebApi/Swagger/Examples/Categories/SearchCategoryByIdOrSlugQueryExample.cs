using MarketNet.Application.Categories.Queries;
using Swashbuckle.AspNetCore.Filters;

namespace MarketNet.WebApi.Swagger.Examples.Categories;

public class SearchCategoryByIdOrSlugQueryExample : IExamplesProvider<SearchCategoryByIdOrSlugQuery>
{
    public SearchCategoryByIdOrSlugQuery GetExamples() => new SearchCategoryByIdOrSlugQuery
    {
        Id = 1,
        Slug = "electronica"
    };
}
