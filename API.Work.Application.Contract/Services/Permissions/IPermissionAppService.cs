
using API.Work.Application.Contract.Common;
using API.Work.Application.Contract.Services.Roles;

namespace API.Work.Application.Contract.Services.Permissions;

public interface IPermissionAppService
{
    Task<ApiResponse<PermissionDto>> GetAsync(Guid id);

    Task<ApiResponse<List<PermissionDto>>> GetListAsync(GetPermissionListDto input);

    Task<Guid> CreateAsync(CreatePermissionDto input);

    Task<bool> UpdateAsync(Guid id, UpdatePermissionDto input);

    Task<bool> DeleteAsync(Guid id);
}
