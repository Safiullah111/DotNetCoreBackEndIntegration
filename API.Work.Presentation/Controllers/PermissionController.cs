using API.Work.Application.Commands.Permissions;
using API.Work.Application.Contract.Common;
using API.Work.Application.Contract.Requests;
using API.Work.Application.Contract.Services.Permissions;
using API.Work.Application.Queries.permissions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Work.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PermissionController : ControllerBase
{
    private readonly IMediator _mediator;

    public PermissionController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("create-permission")]
    //[AuthorizePermission(Permissions.Permissions.Create)]
    public async Task<Guid> CreatePermission(CreatePermissionDto request)
    {
        return await _mediator.Send(new CreatePermissionCommand(request));
    }

    [HttpGet("get-permission-by-id/{id}")]
    public async Task<PermissionDto?> GetPermissionById(Guid id)
    {
        return await _mediator.Send(new GetPermissionByIdQueryRequest(id));
    }

    [HttpGet("get-permission-list")]
    public async Task<ActionResult<ApiResponse<List<PermissionDto>>>> GetPermissionListAsync(GetPermissionListDto permissionListDto)
    {
        return await _mediator.Send(new GetAllPermissionQueryRequest(permissionListDto));
    }

    [HttpPut("update-permission")]
    public async Task<bool> UpdatePermissionAsync(Guid id,UpdatePermissionDto update)
    {
        return await _mediator.Send(new UpdatePermissionCommand(id, update));
    }
}
