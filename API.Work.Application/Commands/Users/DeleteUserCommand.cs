using API.Work.Application.Contract.Common;
using MediatR;

namespace API.Work.Application.Commands.Users;

public class DeleteUserCommand : IRequest<ApiResponse<bool>>
{
    public Guid Id { get; set; }
    public DeleteUserCommand(Guid id) => Id = id;
}
