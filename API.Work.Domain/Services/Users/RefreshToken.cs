using API.Work.Domain.Shared.Common.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Work.Domain.Services.Users;

public class RefreshToken : BaseEntity
{
    [ForeignKey("UserId")]
    public Guid UserId { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiryDate { get; set; }
    public bool IsRevoked { get; set; } = false;
    public User? User { get; set; }
}

