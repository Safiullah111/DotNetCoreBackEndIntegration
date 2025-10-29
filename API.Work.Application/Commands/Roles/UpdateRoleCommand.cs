using API.Work.Application.Contract.Common;
using API.Work.Application.Contract.Services.Roles;
using MediatR;


namespace API.Work.Application.Commands.Roles;

public class UpdateRoleCommand : IRequest<ApiResponse<bool>>
{
    public Guid Id { get; set; }
    public UpdateRoleDto UpdateRoleDto { get; set; }

    public UpdateRoleCommand(Guid id, UpdateRoleDto updateRoleDto)
    {
        Id = id;
        UpdateRoleDto = updateRoleDto;
    }
}
