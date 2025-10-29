

using API.Work.Application.Contract.Common;
using API.Work.Application.Contract.Services.JwtSettings;

namespace API.Work.Application.Contract.Services.Authentication;

public interface ILoginAppService
{
    Task<ApiResponse<JwtToken>> LoginAsync(LoginRequestDto login);
    Task<ApiResponse<JwtToken>> LogoutAsync(Guid userId);
    Task<ApiResponse<JwtToken>> RefreshTokenAsync(RefreshTokenRequestDto requestDto);
}
