using API.Work.Application.Commands.Users;
using API.Work.Application.Contract.Common;
using API.Work.Application.Contract.Enums;
using API.Work.Application.Contract.Requests;
using API.Work.Application.Contract.Services.Users;
using API.Work.Application.Queries.users;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Work.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpPost("create-user")]
    public async Task<ActionResult<ApiResponse<Guid>>> CreateUser(CreateUserDto request)
    {

        return Ok(await _mediator.Send(new CreateUserCommand(request)));
    }

    [HttpGet("get-user-by/{id}")]
    public async Task<ActionResult<ApiResponse<UserDto>>> GetUserById(Guid id)
    {

        return Ok(await _mediator.Send(new GetUserByIdQueryRequest(id)));
    }

    [HttpGet("get-user-list")]
    public async Task<ActionResult<ApiResponse<List<UserDto>>>> GetUserListAsync(GetUserListDto userListDto)
    {

        return Ok(await _mediator.Send(new GetAllUserQueryRequest(userListDto)));
    }

    [HttpPut("update-user")]
    public async Task<ActionResult<ApiResponse<bool>>> UpdateUserAsync(Guid id, UpdateUserDto update)
    {

        return Ok(await _mediator.Send(new UpdateUserCommand(id, update)));
    }

    [HttpDelete("delete-user/{id}")]
    public async Task<ActionResult<ApiResponse<bool>>> DeleteUserAsync(Guid id)
    {

        return Ok(await _mediator.Send(new DeleteUserCommand(id)));
    }
}
