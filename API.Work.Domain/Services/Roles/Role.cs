using API.Work.Domain.Services.Permissions;
using API.Work.Domain.Shared.Common.BaseEntity;
using System.ComponentModel.DataAnnotations;

namespace API.Work.Domain.Services.Roles;

public class Role : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<UserRole>? UserRoles { get; set; }
    public ICollection<RolePermission>? RolePermissions { get; set; }
    public Role(Guid id, string name, string description) : base(id)
    {
        Id = id;
        Name = name;
        Description = description;
    }
  
}
