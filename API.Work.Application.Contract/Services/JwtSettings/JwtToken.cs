

namespace API.Work.Application.Contract.Services.JwtSettings;

public class JwtToken
{
    public string token { get; set; }
    public string expiry { get; set; }
    public string refreshToken { get; set; }
}
