using API.Work.Application.Contract.Common;
using API.Work.Application.Contract.Services.Roles;
using MediatR;


namespace API.Work.Application.Queries.roles;

public class GetAllRoleQueryHandler : IRequestHandler<GetAllRoleQueryRequest, ApiResponse<List<RoleDto>>>
{
    public readonly IRoleAppService _roleAppServices;
    public GetAllRoleQueryHandler(IRoleAppService roleAppServices)
    {
        _roleAppServices = roleAppServices;
    }
    public Task<ApiResponse<List<RoleDto>>> Handle(GetAllRoleQueryRequest request, CancellationToken cancellationToken)
    {
        return _roleAppServices.GetListAsync(request.GetRoleListDto);
    }
}
