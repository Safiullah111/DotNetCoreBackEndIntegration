using API.Work.Domain.Shared.Common.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Work.Domain.Services.Users;

public class UserLogin : BaseEntity
{
    [ForeignKey("UserId")]
    public Guid? UserId { get; set; }
    public string? Email { get; set; }
    public bool IsSuccessful { get; set; }
    public string IPAddress { get; set; } = string.Empty;
    public string? UserAgent { get; set; }
    public DateTime LoginTime { get; set; } = DateTime.UtcNow;
    public User? User { get; set; }
}

