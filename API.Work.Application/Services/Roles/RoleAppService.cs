using API.Work.Application.Common.Mapping;
using API.Work.Application.Configurations.GenericServices;
using API.Work.Application.Contract.Common;
using API.Work.Application.Contract.Common.Expections.BusinessExceptions;
using API.Work.Application.Contract.Services.Roles;
using API.Work.Domain.Configurations.GenericRepository;
using API.Work.Domain.Services.Permissions;
using API.Work.Domain.Services.Roles;
using API.Work.Domain.Shared;

namespace API.Work.Application.Services.Roles;

public class RoleAppService : AppServiceBase<Role>, IRoleAppService
{
    public readonly IRepositoryBase<Role> _roleRepository;
    public readonly IRepositoryBase<Permission> _permissionREpostory;
    public RoleAppService(IRepositoryBase<Role> repository, IRepositoryBase<Permission> permissionREpostory) : base(repository)
    {
        _roleRepository = repository;
        _permissionREpostory = permissionREpostory;
    }

    public async Task<ApiResponse<Guid>> CreateAsync(CreateRoleDto input)
    {
        var id = await _roleRepository.AddAsync(new Role(Guid.NewGuid(), input.Name, input.Description));
        if (id == Guid.Empty)
        {
            throw new RoleAppServicesException(input.Name, APIWorkDomainCode.RoleCreationFailed);
        }

        return ApiResponse<Guid>.Ok(id, APIWorkDomainCode.RoleSuccessfullyCreated);
    }

    public async Task<ApiResponse<RoleDto>> GetAsync(Guid id)
    {
        Role? role = await _roleRepository.GetByIdAsync(id);

        if (role == null) throw new RoleAppServicesException(id.ToString(), APIWorkDomainCode.RoleNotFound);

        return ApiResponse<RoleDto>.Ok(ObjectMapper.Mapper.Map<Role, RoleDto>(role));
    }

    public async Task<ApiResponse<List<RoleDto>>> GetListAsync(GetRoleListDto input)
    {
        List<Role> roles = await _roleRepository.GetListAsync();
        List<RoleDto> respons = ObjectMapper.Mapper.Map<List<RoleDto>>(roles);

        return ApiResponse<List<RoleDto>>.Ok(respons, APIWorkDomainCode.RoleRetrievedSuccessfully);

    }

    public async Task<ApiResponse<bool>> UpdateAsync(Guid id, UpdateRoleDto input)
    {
        var role = await _roleRepository.GetByIdAsync(id);
        if (role == null) throw new RoleAppServicesException(id.ToString(), APIWorkDomainCode.RoleNotFound);
        role.Name = input.Name;
        role.Description = input.Description;
        if (!await _roleRepository.UpdateAsync(role))
        {
            throw new RoleAppServicesException(id.ToString(), APIWorkDomainCode.RoleSuccessfullyUpdated);
        }

        return ApiResponse<bool>.Ok(true);
    }

    public new async Task<ApiResponse<bool>> DeleteAsync(Guid id)
    {
        var role = await _roleRepository.GetByIdAsync(id);
        if (role == null) throw new RoleAppServicesException(id.ToString(), APIWorkDomainCode.RoleNotFound);
        if (!await _roleRepository.DeleteAsync(id))
        {
            throw new RoleAppServicesException(id.ToString(), APIWorkDomainCode.RoleDeletionFailed);
        }

        return ApiResponse<bool>.Ok(true);
    }
}
