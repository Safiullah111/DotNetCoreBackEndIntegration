using API.Work.Application.Contract.Services.Permissions;
using MediatR;


namespace API.Work.Application.Contract.Requests;

public class GetPermissionByIdQueryRequest : IRequest<PermissionDto>
{
    public Guid PermissionId { get; set; }

    public GetPermissionByIdQueryRequest(Guid permissionId)
    {
        PermissionId = permissionId;
    }
}
