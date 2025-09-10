using MarketApp.src.Domain.entities.product;
using MarketNet.src.Application.Products.Dto;
using MarketNet.src.Application.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MarketNet.src.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Product>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<List<ProductDto>>> SearchByFilter([FromQuery] SearchProductsQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpGet("searchByIdentifier")]
        public async Task<ActionResult<ProductDto>> Search(
           [FromQuery] SearchProductsByProductCodeOrIdQuery request
       )
        {
            return await _mediator.Send(request);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(long))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<long>> create(CreateProductCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(long))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<ProductDto>> update(UpdateProductCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
