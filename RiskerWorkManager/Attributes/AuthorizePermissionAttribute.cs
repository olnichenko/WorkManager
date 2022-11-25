using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RiskerWorkManager.Services;
using WorkManagerDal.Models;
using WorkManagerDal.Services;

namespace RiskerWorkManager.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizePermissionAttribute : Attribute, IAuthorizationFilter
    {
        private string _permissionName;
        public AuthorizePermissionAttribute(string? permissionName = null)
        {
            _permissionName = permissionName;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // skip authorization if action is decorated with [AllowAnonymous] attribute
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;

            var identityService = context.HttpContext.RequestServices.GetService<IUserIdentityService>();
            var permissionService = context.HttpContext.RequestServices.GetService<IPermissionsService>();
                   
            var result = identityService.IsUserLoggedIn(context.HttpContext);
            if (!result)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                return;
            }
            if (!string.IsNullOrEmpty(_permissionName))
            {
                var user = identityService.GetCurrentUser(context.HttpContext);
                result = permissionService.IsUserHaveAcces(user, _permissionName);
                if (!result)
                {
                    context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                }
            }
        }
    }
}
