using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace ShopManagementSystem.Services.UserServices
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class AuthorizeAdministratorAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _role;

        public AuthorizeAdministratorAttribute(string role)
        {
            _role = role;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authorized = context.HttpContext.User.Identity?.IsAuthenticated ?? false;
            if (!authorized)
            {
                // The user is not authenticated
                context.Result = new UnauthorizedResult();
                return;
            }

            var roles = _role.Split(',');

            // Check if the user has the required role
            var user = context.HttpContext.User;
            if (!roles.Any(role => user.IsInRole(role.Trim())))
            {
                // The user is not authorized
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        { "controller", "Error" },
                        { "action", "AccessDenied" }
                    });
            }
        }
    }
}
