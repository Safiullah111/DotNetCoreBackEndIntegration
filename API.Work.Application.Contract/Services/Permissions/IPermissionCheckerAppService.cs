namespace API.Work.Application.Contract.Services.Permissions;

public interface IPermissionCheckerAppService
{
    Task<bool> HasPermissionAsync(Guid userId, string permissionName);
}
