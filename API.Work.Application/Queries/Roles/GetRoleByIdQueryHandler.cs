using API.Work.Application.Common.Mapping;
using API.Work.Application.Contract.Common;
using API.Work.Application.Contract.Requests;
using API.Work.Application.Contract.Services.Roles;
using API.Work.Domain.Services.Roles;
using MediatR;
using System.Runtime.Serialization;


namespace API.Work.Application.Queries.roles;

public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQueryRequest, ApiResponse<RoleDto>>
{
    private readonly IRoleAppService _roleAppService;

    public GetRoleByIdQueryHandler(IRoleAppService roleRoleAppService)
    {
        _roleAppService = roleRoleAppService;
    }

    public async Task<ApiResponse<RoleDto>> Handle(GetRoleByIdQueryRequest request, CancellationToken cancellationToken)
    {
        return await _roleAppService.GetAsync(request.RoleId);
    }

}
