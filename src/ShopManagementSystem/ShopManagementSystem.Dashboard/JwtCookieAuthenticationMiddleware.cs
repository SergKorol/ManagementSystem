using Microsoft.AspNetCore.Mvc;
using ShopManagementSystem.Application.Dependencies;

namespace ShopManagementSystem.Dashboard
{
    [IgnoreAntiforgeryToken]
    public class JwtCookieAuthenticationMiddleware : IMiddleware
    {
        private readonly IUserService _userService;

        public JwtCookieAuthenticationMiddleware(IUserService userService)
            => _userService = userService;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var token = context.Request.Cookies["Token"];
            if (token != null)
            {
               context = await _userService.VerifyToken(context, token);
                context.Request.Headers.Append("Authorization", $"Bearer {token}");
            }

            await next.Invoke(context);
        }
    }

    public static class JwtCookieAuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseJwtCookieAuthentication(this IApplicationBuilder app)
            => app
                .UseMiddleware<JwtCookieAuthenticationMiddleware>();
    }
}
