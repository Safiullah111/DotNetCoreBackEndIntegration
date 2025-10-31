using API.Work.Application.Contract.Common;

namespace API.Work.Application.Contract.Services.Roles;

public interface IRoleAppService
{
    Task<ApiResponse<RoleDto>> GetAsync(Guid id);

    Task<ApiResponse<List<RoleDto>>> GetListAsync(GetRoleListDto input);

    Task<ApiResponse<Guid>> CreateAsync(CreateRoleDto input);

    Task<ApiResponse<bool>> UpdateAsync(Guid id, UpdateRoleDto input);

    Task<ApiResponse<bool>> DeleteAsync(Guid id);
}
