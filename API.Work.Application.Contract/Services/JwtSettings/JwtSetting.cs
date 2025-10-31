
namespace API.Work.Application.Contract.Services.JwtSettings;

public class JwtSetting
{
    public string Key { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public double DurationsInMinutes { get; set; }
}
