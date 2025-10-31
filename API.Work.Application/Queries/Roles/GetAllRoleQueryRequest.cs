using API.Work.Application.Contract.Common;
using API.Work.Application.Contract.Services.Roles;
using MediatR;

namespace API.Work.Application.Queries.roles;

public class GetAllRoleQueryRequest : IRequest<ApiResponse<List<RoleDto>>>
{
    public GetRoleListDto GetRoleListDto { get; set; }
    public GetAllRoleQueryRequest(GetRoleListDto getRoleListDto) => GetRoleListDto = getRoleListDto;

}
