using API.Work.Application.Contract.Common;
using API.Work.Application.Contract.Services.Roles;
using MediatR;


namespace API.Work.Application.Contract.Requests;

public class GetRoleByIdQueryRequest : IRequest<ApiResponse<RoleDto>>
{
    public Guid RoleId { get; set; }

    public GetRoleByIdQueryRequest(Guid roleId)
    {
        RoleId = roleId;
    }
}
