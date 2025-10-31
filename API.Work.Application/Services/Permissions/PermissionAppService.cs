using API.Work.Application.Common.Mapping;
using API.Work.Application.Configurations.GenericServices;
using API.Work.Application.Contract.Common;
using API.Work.Application.Contract.Services.Permissions;
using API.Work.Domain.Configurations.GenericRepository;
using API.Work.Domain.Services.Permissions;

namespace API.Work.Application.Services.Permissions;

public class PermissionAppService : AppServiceBase<Permission>, IPermissionAppService
{
    private readonly IRepositoryBase<Permission> _permissionRepository;
    public PermissionAppService(IRepositoryBase<Permission> repository) : base(repository)
    {
        _permissionRepository = repository;
    }

    public Task<Guid> CreateAsync(CreatePermissionDto input)
    {
        return _permissionRepository.AddAsync(new Permission(Guid.NewGuid(), input.Name));
    }

    public async Task<ApiResponse<PermissionDto>> GetAsync(Guid id)
    {
        Permission? permission = await _permissionRepository.GetByIdAsync(id);
        if (permission == null) throw new Exception("Permission not found");

        return new ApiResponse<PermissionDto>()
        {
            Payload = ObjectMapper.Mapper.Map<PermissionDto>(permission),
            Success = true
        };
    }

    public async Task<ApiResponse<List<PermissionDto>>> GetListAsync(GetPermissionListDto input)
    {
        List<Permission> permissions = await _permissionRepository.GetListAsync();
        List<PermissionDto> response = ObjectMapper.Mapper.Map<List<PermissionDto>>(permissions);

        return  ApiResponse<List<PermissionDto>>.Ok(response, "Permission retrive successfully" );
    }

    public async Task<bool> UpdateAsync(Guid id, UpdatePermissionDto input)
    {
        var permission = await _permissionRepository.GetByIdAsync(id);
        if (permission == null) throw new Exception("Permission not found");
        permission.Name = input.Name;

        return await _permissionRepository.UpdateAsync(permission);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _permissionRepository.DeleteAsync(id);
    }
}
