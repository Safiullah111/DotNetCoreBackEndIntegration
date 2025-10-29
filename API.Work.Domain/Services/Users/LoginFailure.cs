using API.Work.Domain.Shared.Common.BaseEntity;

namespace API.Work.Domain.Services.Users;

public class LoginFailure : BaseEntity
{
    public string Email { get; set; } = string.Empty;
    public string IPAddress { get; set; } = string.Empty;
    public string Reason { get; set; } = string.Empty;
    public DateTime AttemptTime { get; set; } = DateTime.UtcNow;
}
