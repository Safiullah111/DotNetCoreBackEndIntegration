

using API.Work.Application.Contract.Services.JwtSettings;
using API.Work.Domain.Services.Users;

namespace API.Work.Application.Services.JwtSettings;

public interface IJwtTokenGenerator
{
    JwtToken GenerateToken(User user);
}
