using API.Work.Application.Contract.Services.Permissions;
using MediatR;

namespace API.Work.Application.Commands.Permissions;

public class CreatePermissionCommand : IRequest<Guid>
{
    public CreatePermissionDto CreatePermissionDto { get; set; }
    public CreatePermissionCommand(CreatePermissionDto createUser) => CreatePermissionDto = createUser;
}
