using API.Work.Application.Contract.Common;
using API.Work.Application.Contract.Services.Permissions;
using MediatR;


namespace API.Work.Application.Queries.permissions;

public class GetAllPermissionQueryHandler : IRequestHandler<GetAllPermissionQueryRequest, ApiResponse<List<PermissionDto>>>
{
    public readonly IPermissionAppService _permissionAppServices;
    public GetAllPermissionQueryHandler(IPermissionAppService permissionAppServices)
    {
        _permissionAppServices = permissionAppServices;
    }
    public Task<ApiResponse<List<PermissionDto>>> Handle(GetAllPermissionQueryRequest request, CancellationToken cancellationToken)
    {
        return _permissionAppServices.GetListAsync(request.GetPermissionListDto);
    }
}
