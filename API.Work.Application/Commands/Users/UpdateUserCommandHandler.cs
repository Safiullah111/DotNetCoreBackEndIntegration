using API.Work.Application.Contract.Common;
using API.Work.Application.Contract.Services.Users;
using MediatR;

namespace API.Work.Application.Commands.Users;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ApiResponse<bool>>
{
    public readonly IUserAppServices _userAppServices;
    public UpdateUserCommandHandler(IUserAppServices userAppServices)
    {
        _userAppServices = userAppServices;
    }
    public async Task<ApiResponse<bool>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        return await _userAppServices.UpdateAsync(request.Id, request.UpdateUserDto);
    }
}
