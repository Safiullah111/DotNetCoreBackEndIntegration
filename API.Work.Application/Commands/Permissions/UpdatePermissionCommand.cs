using API.Work.Application.Contract.Services.Permissions;
using MediatR;


namespace API.Work.Application.Commands.Permissions;

public class UpdatePermissionCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public UpdatePermissionDto UpdatePermissionDto { get; set; }

    public UpdatePermissionCommand(Guid id, UpdatePermissionDto updatePermissionDto)
    {
        Id = id;
        UpdatePermissionDto = updatePermissionDto;
    }
}
