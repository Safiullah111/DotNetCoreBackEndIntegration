using API.Work.Domain.Services.Users.UserPermissions;
using API.Work.Domain.Shared.Common.BaseEntity;

namespace API.Work.Domain.Services.Permissions;

public class Permission : BaseEntity
{
        public string Name { get; set; }
    public ICollection<RolePermission>? RolePermissions { get; set; }
    public ICollection<UserPermission>? UserPermissions { get; set; }

    public Permission(Guid id, string name) : base(id)
    {
        Id = id;
        Name = name;
    }
}
