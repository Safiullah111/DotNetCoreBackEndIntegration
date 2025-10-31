using MediatR;

namespace API.Work.Application.Commands.Permissions;

public class DeletePermissionCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public DeletePermissionCommand(Guid id) => Id = id;
}
