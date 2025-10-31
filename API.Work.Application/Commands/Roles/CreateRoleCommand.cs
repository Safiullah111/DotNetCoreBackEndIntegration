using API.Work.Application.Contract.Common;
using API.Work.Application.Contract.Services.Roles;
using MediatR;

namespace API.Work.Application.Commands.Roles;

public class CreateRoleCommand : IRequest<ApiResponse<Guid>>
{
    public CreateRoleDto CreateRoleDto { get; set; }
    public CreateRoleCommand(CreateRoleDto createUser) => CreateRoleDto = createUser;
}
