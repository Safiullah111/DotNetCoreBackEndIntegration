

using API.Work.Application.Commands.Permissions;
using API.Work.Application.Contract.Common;
using API.Work.Application.Contract.Services.Authentication;
using API.Work.Application.Contract.Services.JwtSettings;
using MediatR;

namespace API.Work.Application.Commands.Authentication;

public class CreateRefreshCommandHandler : IRequestHandler<CreateRefreshTokenCommand, ApiResponse<JwtToken>>
{
    public readonly ILoginAppService loginAppService;
    public CreateRefreshCommandHandler(ILoginAppService loginAppService)
    {
        this.loginAppService = loginAppService;
    }
    public async Task<ApiResponse<JwtToken>> Handle(CreateRefreshTokenCommand request, CancellationToken cancellationToken)
    {
            return await loginAppService.RefreshTokenAsync(request.RefreshToken);
    }
}
