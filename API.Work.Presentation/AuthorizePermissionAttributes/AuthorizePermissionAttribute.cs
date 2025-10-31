using API.Work.Application.Contract.Services.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Work.Presentation.AuthorizePermissionAttributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class AuthorizePermissionAttribute : Attribute, IAsyncAuthorizationFilter
{
    private readonly string _permission;

    public AuthorizePermissionAttribute(string permission)
    {
        _permission = permission;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var userIdClaim = context.HttpContext.User.FindFirst("sub") ?? context.HttpContext.User.FindFirst("UserId");
        if (userIdClaim == null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var userId = Guid.Parse(userIdClaim.Value);
        var permissionChecker = context.HttpContext.RequestServices.GetService<IPermissionCheckerAppService>();

        if (!await permissionChecker.HasPermissionAsync(userId, _permission))
        {
            context.Result = new ForbidResult();
        }
    }
}
