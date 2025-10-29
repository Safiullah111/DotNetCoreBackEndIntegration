using API.Work.Application.Contract.Services.JwtSettings;
using API.Work.Application.Contract.Services.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using API.Work.Domain.Services.Users;


namespace API.Work.Application.Services.JwtSettings;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSetting _jwtSettings;
    public JwtTokenGenerator(IOptions<JwtSetting> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }
    public JwtToken GenerateToken(User user)
    {
        var clamis = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
            new Claim(JwtRegisteredClaimNames.Email,user.UserEmail),
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),

        };

        List<Claim> roleClaims = user?.UserRoles != null ? user.UserRoles.Where(x => x.Role != null).Select(r => new Claim(ClaimTypes.Role, r.Role.Name)).ToList() : new List<Claim>();
        List<Claim> permissionClaims = user?.UserPermissions != null ? user.UserPermissions.Where(x => x.Permission != null).Select(p =>  new Claim("permission", p.Permission.Name)).ToList() : new List<Claim>();

        clamis.AddRange(roleClaims);
        clamis.AddRange(permissionClaims);

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_jwtSettings.Key));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: clamis,
            expires: DateTime.Now.AddMinutes(_jwtSettings.DurationsInMinutes),
            signingCredentials: creds
            );

        JwtToken tokenReponse = new JwtToken();
        tokenReponse.token = new JwtSecurityTokenHandler().WriteToken(token);
        tokenReponse.expiry = token.ValidTo.ToString("dd-MM-yyyy hh:mm:ss");

        return tokenReponse;
    }
}
