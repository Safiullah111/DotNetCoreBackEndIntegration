using API.Work.Domain.Services.Permissions;
using API.Work.Domain.Services.Users;
using API.Work.Domain.Shared.Common.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Work.Domain.Services.Users.UserPermissions;

public class UserPermission : BaseEntity
{
    [ForeignKey(nameof(UserId))]
    public Guid UserId { get; set; }
    public User User { get; set; }
    [ForeignKey(nameof(PermissionId))]
    public Guid PermissionId { get; set; }
    public Permission? Permission { get; set; }
    public UserPermission(Guid id, Guid userId, Guid permissionId) : base(id)
    {
        PermissionId = permissionId;
        UserId = userId;
    }
}
