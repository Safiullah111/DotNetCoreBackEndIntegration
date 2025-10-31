
using API.Work.Application.Contract.Common;
using API.Work.Application.Contract.Services.Authentication;
using API.Work.Application.Contract.Services.JwtSettings;
using MediatR;

namespace API.Work.Application.Commands.Authentication;

public class CreatedLogOutCommandHandler : IRequestHandler<CreateLogOutCommand, ApiResponse<JwtToken>>
{
    private readonly ILoginAppService _loginAppService;
    public CreatedLogOutCommandHandler(ILoginAppService loginAppService)
    {
        _loginAppService = loginAppService;
    }
    public async Task<ApiResponse<JwtToken>> Handle(CreateLogOutCommand request, CancellationToken cancellationToken)
    {
        return await _loginAppService.LogoutAsync(request.Id);
    }
}
