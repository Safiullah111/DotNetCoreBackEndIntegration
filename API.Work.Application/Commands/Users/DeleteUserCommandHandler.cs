using API.Work.Application.Contract.Common;
using API.Work.Application.Contract.Services.Users;
using MediatR;

namespace API.Work.Application.Commands.Users;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ApiResponse<bool>>
{
    public readonly IUserAppServices _userAppServices;
    public DeleteUserCommandHandler(IUserAppServices userAppServices)
    {
        _userAppServices = userAppServices;
    }
    public Task<ApiResponse<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        return _userAppServices.DeleteAsync(request.Id);
    }
}
