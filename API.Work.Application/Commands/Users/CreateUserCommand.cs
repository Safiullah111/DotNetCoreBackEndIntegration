using API.Work.Application.Contract.Common;
using API.Work.Application.Contract.Services.Users;
using MediatR;

namespace API.Work.Application.Commands.Users;

public class CreateUserCommand : IRequest<ApiResponse<Guid>>
{
    public CreateUserDto CreateUserDto { get; set; }
    public CreateUserCommand(CreateUserDto createUser) => CreateUserDto = createUser;
}
