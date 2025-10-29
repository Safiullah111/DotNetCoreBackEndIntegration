

namespace API.Work.Application.Contract.Services.Authentication;

public class RefreshTokenRequestDto
{
    public string Token { get; set; }
    public string UserEmail { get; set; }
    public string UserName { get; set; }
}