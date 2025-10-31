
using API.Work.Application.Contract.Common;
using API.Work.Application.Contract.Services.Authentication;
using API.Work.Application.Contract.Services.JwtSettings;
using MediatR;

namespace API.Work.Application.Commands.Authentication;

public class CreateRefreshTokenCommand : IRequest<ApiResponse<JwtToken>>
{
    public  RefreshTokenRequestDto RefreshToken { get; set; }
    public CreateRefreshTokenCommand(RefreshTokenRequestDto refreshToken) => RefreshToken = refreshToken;
}
