using API.Work.Application.Contract.Common;
using API.Work.Application.Contract.Services.Users;
using MediatR;


namespace API.Work.Application.Commands.Users;

public class UpdateUserCommand : IRequest<ApiResponse<bool>>
{
    public Guid Id { get; set; }
    public UpdateUserDto UpdateUserDto { get; set; }

    public UpdateUserCommand(Guid id, UpdateUserDto updateUserDto)
    {
        Id = id;
        UpdateUserDto = updateUserDto;
    }
}
