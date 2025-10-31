using API.Work.Application.Contract.Common;
using API.Work.Application.Contract.Services.Users;
using MediatR;


namespace API.Work.Application.Commands.Users;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ApiResponse<Guid>>
{
    private readonly IUserAppServices _userAppService;
    public CreateUserCommandHandler(IUserAppServices userAppService)
    {
        _userAppService = userAppService;
    }
    public async Task<ApiResponse<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        return await _userAppService.CreateAsync(request.CreateUserDto);
    }
}
