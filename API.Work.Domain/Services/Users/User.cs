using API.Work.Domain.Services.Roles;
using API.Work.Domain.Services.Users.UserPermissions;
using API.Work.Domain.Shared.Common.BaseEntity;
using System.ComponentModel.DataAnnotations;


namespace API.Work.Domain.Services.Users;

public class User : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string UserName { get; set; }

    [Required]
    [MaxLength(100)]
    [EmailAddress]
    public string UserEmail { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; }

    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }   

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }

    [Required]
    public bool IsActive { get; set; }

    [Required]
    public AccessLevel AccessLevel { get; set; }
    public DateTime? LockoutEnd { get; set; }
    public int AccessFailedCount { get; set; }

    public ICollection<UserRole>? UserRoles { get; set; }
    public ICollection<UserPermission>? UserPermissions { get; set; }
    public ICollection<UserLogin>? UserLogins { get; set; }
    public ICollection<RefreshToken>? RefreshTokens { get; set; }

    public User( Guid id, string userName, string userEmail, string passwordHash, string firstName, string lastName, bool isActive,AccessLevel accessLevel):base(id)
    {
        UserName = userName;
        UserEmail = userEmail;
        PasswordHash = passwordHash;
        FirstName = firstName;
        LastName = lastName;
        IsActive = isActive;
        AccessLevel = accessLevel;
    }
    public User()
    {

    }
}
