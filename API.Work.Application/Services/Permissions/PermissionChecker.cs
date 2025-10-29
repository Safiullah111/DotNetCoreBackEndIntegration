using API.Work.Application.Configurations.GenericServices;
using API.Work.Application.Contract.Services.Permissions;
using API.Work.Domain.Configurations.GenericRepository;
using API.Work.Domain.Services.Permissions;
using API.Work.Domain.Services.Roles;
using API.Work.Domain.Services.Users;
using API.Work.Domain.Services.Users.UserPermissions;
using Microsoft.EntityFrameworkCore;
using System;

namespace API.Work.Application.Services.Permissions;

public class PermissionChecker : AppServiceBase<User>, IPermissionCheckerAppService
{
    public readonly IUserRepository _userRepository;
    public readonly IRepositoryBase<UserRole> _userRoleRepository;
    public readonly IRepositoryBase<RolePermission> _rolePermissionRepository;
    public readonly IRepositoryBase<UserPermission> _userPermissionRepostiroy;
    public PermissionChecker(IUserRepository userRepository,
        IRepositoryBase<UserRole> userRoleRepository,
        IRepositoryBase<RolePermission> rolePermissionRepository,
        IRepositoryBase<UserPermission> userPermissionRepostiroy) : base(userRepository)
    {
        _userRepository = userRepository;
        _userRoleRepository = userRoleRepository;
        _rolePermissionRepository = rolePermissionRepository;
        _userPermissionRepostiroy = userPermissionRepostiroy;
    }

    public async Task<bool> HasPermissionAsync(Guid userId, string permissionName)
    {
        var userRoles = await (await _userRoleRepository.GetAll())
            .Where(ur => ur.UserId == userId)
            .Select(ur => ur.RoleId).ToListAsync();

        var hasRolePermission =await (await _rolePermissionRepository.GetAll()).Include(rp => rp.Permission)
            .AnyAsync( rp =>  userRoles.Contains(rp.RoleId) && rp.Permission.Name == permissionName);

        var hasUserPermission = await (await _userPermissionRepostiroy.GetAll())
            .Include(up => up.Permission)
            .AnyAsync(up => up.UserId == userId && up.Permission.Name == permissionName);

        return hasRolePermission || hasUserPermission;
    }
}

