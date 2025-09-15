using MarketNet.src.Application.Auth.Commands;
using MarketNet.src.Application.Auth.Dto;
using MarketNet.src.Application.Auth.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<LoginResult> Login(LoginQuery request)
    {
        return await _mediator.Send(request);
    }

    [HttpPost("register/seller")]
    public async Task<ActionResult<LoginResult>> RegisterSeller(RegisterSellerCommand request)
    {
        return await _mediator.Send(request);
    }

    [HttpPost("register/customer")]
    public async Task<ActionResult<LoginResult>> RegisterCustomer(RegisterCustomerCommand request)
    {
        return await _mediator.Send(request);
    }

}
