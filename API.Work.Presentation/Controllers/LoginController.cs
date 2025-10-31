using API.Work.Application.Commands.Authentication;
using API.Work.Application.Contract.Common;
using API.Work.Application.Contract.Services.Authentication;
using API.Work.Application.Contract.Services.JwtSettings;
using API.Work.Application.Queries.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace API.Work.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private IMediator _mediator;
    public LoginController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<ApiResponse<JwtToken>>> LoginAsync([FromBody] LoginRequestDto login)
    {
        return Ok(await _mediator.Send(new LoginQueryRequest(login)));
    }



    [Authorize]
    [HttpPost("log-out")]
    public async Task<ActionResult<ApiResponse<JwtToken>>> LogOutAsync()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // or JwtRegisteredClaimNames.Sub

        return Ok(await _mediator.Send(new CreateLogOutCommand(Guid.Parse(userId))));
    }



    [AllowAnonymous]
    [HttpPost("refresh-token")]
    public async Task<ActionResult<ApiResponse<JwtToken>>> RefreshTokenAsync(RefreshTokenRequestDto refresh)
    {
        return Ok(await _mediator.Send(new CreateRefreshTokenCommand(refresh)));

    }
}
