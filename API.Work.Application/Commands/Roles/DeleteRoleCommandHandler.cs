using API.Work.Application.Contract.Common;
using API.Work.Application.Contract.Services.Roles;
using MediatR;

namespace API.Work.Application.Commands.Roles;

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, ApiResponse<bool>>
{
    public readonly IRoleAppService _roleAppServices;
    public DeleteRoleCommandHandler(IRoleAppService roleAppServices)
    {
        _roleAppServices = roleAppServices;
    }
    public Task<ApiResponse<bool>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        return _roleAppServices.DeleteAsync(request.Id);
    }
}
