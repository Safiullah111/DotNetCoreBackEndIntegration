using API.Work.Application.Contract.Common;
using API.Work.Application.Contract.Services.Permissions;
using MediatR;

namespace API.Work.Application.Queries.permissions;

public class GetAllPermissionQueryRequest : IRequest<ApiResponse<List<PermissionDto>>>
{
    public GetPermissionListDto GetPermissionListDto { get; set; }
    public GetAllPermissionQueryRequest(GetPermissionListDto getPermissionListDto) => GetPermissionListDto = getPermissionListDto;

}
