
using API.Work.Application.Contract.Common;
using API.Work.Application.Contract.Services.Authentication;
using API.Work.Application.Contract.Services.JwtSettings;
using MediatR;

namespace API.Work.Application.Queries.Authentication;

public class LoginQueryHandler : IRequestHandler<LoginQueryRequest, ApiResponse<JwtToken>>
{
    private readonly  ILoginAppService _loginAppService;
    public LoginQueryHandler(ILoginAppService loginAppService)
    {
        _loginAppService = loginAppService;
    }
    public async Task<ApiResponse<JwtToken>> Handle(LoginQueryRequest request, CancellationToken cancellationToken)
    {
        return await  _loginAppService.LoginAsync(request.LoginRequestDto);
    }
}
