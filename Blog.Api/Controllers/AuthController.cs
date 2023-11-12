using Blog.Api.Business.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator mediator;

    public AuthController(IMediator mediator) => this.mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUser registerUser)
    {
        var result = await mediator.Send(registerUser);
        return !result.Succeeded ? new BadRequestObjectResult(result) : Ok();
    }
    
    [HttpPost("login")]
    public async Task<AuthenticateUser.Result> Authenticate([FromBody] AuthenticateUser request)
    {
        var result = await mediator.Send(request);
        if (!result.Succesful)
            new UnauthorizedResult();
        return result;
    }
    
}