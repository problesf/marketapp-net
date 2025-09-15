using MarketNet.Application.Categories.Commands;
using MarketNet.Application.Categories.Dto;
using MarketNet.Application.Categories.Queries;
using MarketNet.WebApi.Swagger.Examples.Categories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace MarketNet.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CategoryDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        [SwaggerRequestExample(typeof(SearchCategoriesQuery), typeof(SearchCategoriesQueryExample))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(CategoryListExample))]
        public async Task<ActionResult<List<CategoryDto>>> Search([FromQuery] SearchCategoriesQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpGet("searchByIdentifier")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        [SwaggerRequestExample(typeof(SearchCategoryByIdOrSlugQuery), typeof(SearchCategoryByIdOrSlugQueryExample))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(CategoryDtoExample))]
        public async Task<ActionResult<CategoryDto>> SearchByIdentifiers([FromQuery] SearchCategoryByIdOrSlugQuery request)
        {
            return await _mediator.Send(request);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(long))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<long>> Create(CreateCategoryCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<CategoryDto>> Update([FromRoute] long id, UpdateCategoryCommand command)
        {
            command.Id = id;
            return await _mediator.Send(command);
        }
    }
}
