using MarketNet.Application.Categories.Commands;
using MarketNet.Application.Categories.Dto;
using MarketNet.Application.Categories.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<List<CategoryDto>>> search([FromQuery] SearchCategoriesQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpGet("searchByIdentifier")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<CategoryDto>> Search([FromQuery] SearchCategoryByIdOrSlugQuery request)
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

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<CategoryDto>> Update(UpdateCategoryCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
