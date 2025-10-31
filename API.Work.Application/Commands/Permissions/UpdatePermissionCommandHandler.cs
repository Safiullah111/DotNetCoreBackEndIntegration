using API.Work.Application.Contract.Services.Permissions;
using MediatR;

namespace API.Work.Application.Commands.Permissions;

public class UpdatePermissionCommandHandler : IRequestHandler<UpdatePermissionCommand, bool>
{
    public readonly IPermissionAppService _permissionAppServices;
    public UpdatePermissionCommandHandler(IPermissionAppService permissionAppServices)
    {
        _permissionAppServices = permissionAppServices;
    }
    public async Task<bool> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
    {
        return await _permissionAppServices.UpdateAsync(request.Id, request.UpdatePermissionDto);
    }
}
