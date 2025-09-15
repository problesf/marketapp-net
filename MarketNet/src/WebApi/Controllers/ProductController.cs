using MarketNet.Application.Products.Commands;
using MarketNet.Application.Products.Dto;
using MarketNet.Application.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketNet.WebApi.Controllers
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProductDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<List<ProductDto>>> SearchByFilter([FromQuery] SearchProductsQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpGet("searchByIdentifier")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<ProductDto>> Search(
           [FromQuery] SearchProductsByProductCodeOrIdQuery request
       )
        {
            return await _mediator.Send(request);
        }

        [HttpPost]
        [Authorize(Roles = "Seller")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(long))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<long>> Create(CreateProductCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("{id:long}")]
        [Authorize(Policy = "ProductOwnerOnly")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<ProductDto>> Update([FromRoute] long id, UpdateProductCommand command)
        {
            command.Id = id;
            return await _mediator.Send(command);
        }

        [HttpDelete("{id:long}")]
        [Authorize(Policy = "ProductOwnerOnly")]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> DeleteByCode(
            [FromRoute] long id,
            CancellationToken cancellationToken)
        {
            return await _mediator.Send(new DeleteProductCommand { Id = id }, cancellationToken);
        }
        [HttpPut("activate/{id:long}")]
        [Authorize(Policy = "ProductOwnerOnly")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<ProductDto>> Activate([FromRoute] long id)
        {
            return await _mediator.Send(new ActivateProductCommand { Id = id });
        }

        [HttpPut("deactivate/{id:long}")]
        [Authorize(Roles = "Seller")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<ProductDto>> Deactivate([FromRoute] long id)
        {

            return await _mediator.Send(new DeactivateProductCommand { Id = id });
        }


    }
}
