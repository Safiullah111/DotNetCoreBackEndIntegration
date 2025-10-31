using API.Work.Application.Contract.Common;
using MediatR;

namespace API.Work.Application.Commands.Roles;

public class DeleteRoleCommand : IRequest<ApiResponse<bool>>
{
    public Guid Id { get; set; }
    public DeleteRoleCommand(Guid id) => Id = id;
}
