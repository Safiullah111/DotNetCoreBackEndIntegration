using API.Work.Application.Commands.Roles;
using API.Work.Application.Contract.Common;
using API.Work.Application.Contract.Requests;
using API.Work.Application.Contract.Services.Roles;
using API.Work.Application.Queries.roles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Work.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly IMediator _mediator;

    public RoleController(IMediator mediator)
    {
        _mediator = mediator;
    }
   // [Authorize]
    [HttpPost("create-role")]
    //[AuthorizePermission(Permissions.Roles.Create)]
    public async Task<ActionResult<ApiResponse<Guid>>> CreateRole(CreateRoleDto request)
    {

        return Ok(await _mediator.Send(new CreateRoleCommand(request)));
    }

    [Authorize]
    [HttpGet("get-role-by-id/{id}")]
    public async Task<ActionResult<ApiResponse<RoleDto>>> GetRoleById(Guid id)
    {
        return Ok(await _mediator.Send(new GetRoleByIdQueryRequest(id)));
    }

    [HttpGet("get-role-list")]
    public async Task<ActionResult<ApiResponse<List<RoleDto>>>> GetRoleListAsync([FromQuery] GetRoleListDto roleListDto)
    {
        return Ok(await _mediator.Send(new GetAllRoleQueryRequest(roleListDto)));
    }
    [Authorize]
    [HttpPut("update-role-by/{id}")]
    public async Task<ActionResult<ApiResponse<bool>>> UpdateRoleAsync(Guid id, UpdateRoleDto roleDto)
    {

        return Ok(await _mediator.Send(new UpdateRoleCommand(id, roleDto)));

    }
}
