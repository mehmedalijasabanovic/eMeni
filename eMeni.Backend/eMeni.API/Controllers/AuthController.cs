using eMeni.Application.Modules.Auth.Commands.Login;
using eMeni.Application.Modules.Auth.Commands.Logout;
using eMeni.Application.Modules.Auth.Commands.Refresh;
using Microsoft.AspNetCore.RateLimiting;

[ApiController]
[Route("api/auth")]
[EnableRateLimiting("AuthPolicy")] // Apply stricter rate limiting to auth endpoints
public sealed class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<LoginCommandDto>> Login([FromBody] LoginCommand command, CancellationToken ct)
    {
        return Ok(await mediator.Send(command, ct));
    }

    [HttpPost("refresh")]
    [AllowAnonymous]
    public async Task<ActionResult<LoginCommandDto>> Refresh([FromBody] RefreshTokenCommand command, CancellationToken ct)
    {
        return Ok(await mediator.Send(command, ct));
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task Logout([FromBody] LogoutCommand command, CancellationToken ct)
    {
        await mediator.Send(command, ct);
    }
}