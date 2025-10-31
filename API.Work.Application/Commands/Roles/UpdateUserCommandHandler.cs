using API.Work.Application.Contract.Common;
using API.Work.Application.Contract.Services.Roles;
using MediatR;

namespace API.Work.Application.Commands.Roles;

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, ApiResponse<bool>>
{
    public readonly IRoleAppService _roleAppServices;
    public UpdateRoleCommandHandler(IRoleAppService roleAppServices)
    {
        _roleAppServices = roleAppServices;
    }
    public async Task<ApiResponse<bool>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        return await _roleAppServices.UpdateAsync(request.Id, request.UpdateRoleDto);
    }
}
