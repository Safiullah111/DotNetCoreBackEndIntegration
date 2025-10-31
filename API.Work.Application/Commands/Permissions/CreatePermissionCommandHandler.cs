using API.Work.Application.Contract.Services.Permissions;
using MediatR;


namespace API.Work.Application.Commands.Permissions;

public class CreatePermissionCommandHandler : IRequestHandler<CreatePermissionCommand, Guid>
{
    private readonly IPermissionAppService _permissionAppService;
    public CreatePermissionCommandHandler(IPermissionAppService permissionAppService)
    {
        _permissionAppService = permissionAppService;
    }
    public async Task<Guid> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
    {
        return await _permissionAppService.CreateAsync(request.CreatePermissionDto);
    }
}
