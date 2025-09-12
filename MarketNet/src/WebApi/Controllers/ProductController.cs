using MarketNet.Application.Products.Commands;
using MarketNet.Application.Products.Dto;
using MarketNet.Application.Products.Queries;
using MediatR;
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(long))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<long>> Create(CreateProductCommand command)
        {
            return await _mediator.Send(command);
        }
       
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<ProductDto>> Update(UpdateProductCommand command)
        {
            return await _mediator.Send(command);
        }
        
        [HttpDelete("{code}")]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> DeleteByCode(
            [FromRoute] string code,
            CancellationToken cancellationToken)
        {
                return await _mediator.Send(new DeleteProductCommand { Code = code }, cancellationToken);
        }
        [HttpPut("/activate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<ProductDto>> Activate(ActivateProductCommand command)
        {
            return await _mediator.Send(command);
        }
        
        [HttpPut("/deactivate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<ProductDto>> Deactivate(DeactivateProductCommand command)
        {
            return await _mediator.Send(command);
        }
        
        
    }
}
