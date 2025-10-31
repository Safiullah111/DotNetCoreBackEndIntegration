using API.Work.Domain.Services.Users;
using API.Work.Domain.Shared.Common.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Work.Domain.Services.Roles;

public class UserRole : BaseEntity
{
    [ForeignKey(nameof(UserId))]
    public Guid UserId { get; set; }
    public User? User { get; set; }

    [ForeignKey(nameof(RoleId))]  
    public Guid RoleId { get; set; }
    public Role? Role { get; set; }

    public UserRole(Guid id,Guid userId, Guid roleId) : base(id)
    {
        RoleId = roleId;
        UserId = userId;
    }

}
