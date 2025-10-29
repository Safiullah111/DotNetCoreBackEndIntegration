using API.Work.Domain.Services.Roles;
using API.Work.Domain.Shared.Common.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Work.Domain.Services.Permissions;

public class RolePermission : BaseEntity
{
    [ForeignKey(nameof(RoleId))]
    public Guid RoleId { get; set; }
    public Role? Role { get; set; }

    [ForeignKey(nameof(PermissionId))]
    public Guid PermissionId { get; set; }
    public Permission? Permission { get; set; }
    public Guid UserId { get; set; }

    public RolePermission(Guid id ,Guid roleId, Guid permissionId,Guid userId) :base(id)
    {
        Id = id;
        UserId = userId;
        RoleId = roleId;
        PermissionId = permissionId;
    }
}
