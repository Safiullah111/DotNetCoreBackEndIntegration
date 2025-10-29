

using API.Work.Application.Contract.Common;
using API.Work.Application.Contract.Services.Authentication;
using API.Work.Application.Contract.Services.JwtSettings;
using MediatR;

namespace API.Work.Application.Queries.Authentication;

public class LoginQueryRequest : IRequest<ApiResponse<JwtToken>>
{
    public LoginRequestDto LoginRequestDto { get; set; }
    public LoginQueryRequest(LoginRequestDto loginRequestDto)
    {
        LoginRequestDto = loginRequestDto;
    }
}
