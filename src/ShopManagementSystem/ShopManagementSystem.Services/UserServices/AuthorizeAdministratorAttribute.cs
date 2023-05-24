using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace ShopManagementSystem.Services.UserServices
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
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
                context.Result = new UnauthorizedResult();
                return;
            }

            var roles = _role.Split(',');

            var user = context.HttpContext.User;
            if (!roles.Any(role => user.IsInRole(role.Trim())))
            {
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
