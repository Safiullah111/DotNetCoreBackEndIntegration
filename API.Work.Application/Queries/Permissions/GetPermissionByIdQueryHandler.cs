using API.Work.Application.Common.Mapping;
using API.Work.Application.Contract.Requests;
using API.Work.Application.Contract.Services.Permissions;
using MediatR;


namespace API.Work.Application.Queries.permissions;

public class GetPermissionByIdQueryHandler : IRequestHandler<GetPermissionByIdQueryRequest, PermissionDto>
{
    private readonly IPermissionAppService _permissionAppService;

    public GetPermissionByIdQueryHandler(IPermissionAppService permissionPermissionAppService)
    {
        _permissionAppService = permissionPermissionAppService;
    }

    public async Task<PermissionDto> Handle(GetPermissionByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var permission = await _permissionAppService.GetAsync(request.PermissionId);

        if (permission == null) return null;

        return ObjectMapper.Mapper.Map<PermissionDto>(permission);
    }

}
