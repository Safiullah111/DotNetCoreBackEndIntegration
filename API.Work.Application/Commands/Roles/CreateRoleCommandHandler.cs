using API.Work.Application.Contract.Common;
using API.Work.Application.Contract.Services.Roles;
using MediatR;


namespace API.Work.Application.Commands.Roles;

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, ApiResponse<Guid>>
{
    private readonly IRoleAppService _roleAppService;
    public CreateRoleCommandHandler(IRoleAppService roleAppService)
    {
        _roleAppService = roleAppService;
    }
    public async Task<ApiResponse<Guid>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        return await _roleAppService.CreateAsync(request.CreateRoleDto);
    }
}
