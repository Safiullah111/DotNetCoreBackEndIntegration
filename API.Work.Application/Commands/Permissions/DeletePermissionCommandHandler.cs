using API.Work.Application.Contract.Services.Permissions;
using MediatR;

namespace API.Work.Application.Commands.Permissions;

public class DeletePermissionCommandHandler : IRequestHandler<DeletePermissionCommand, bool>
{
    public readonly IPermissionAppService _permissionAppServices;
    public DeletePermissionCommandHandler(IPermissionAppService permissionAppServices)
    {
        _permissionAppServices = permissionAppServices;
    }
    public Task<bool> Handle(DeletePermissionCommand request, CancellationToken cancellationToken)
    {
        return _permissionAppServices.DeleteAsync(request.Id);
    }
}
