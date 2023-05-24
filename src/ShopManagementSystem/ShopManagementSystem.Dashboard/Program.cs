using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using ShopManagementSystem.Application;
using ShopManagementSystem.Dashboard;
using ShopManagementSystem.Dashboard.Configuration;
using ShopManagementSystem.Data;
using ShopManagementSystem.Data.Seeds;
using ShopManagementSystem.Services;


var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.RegisterDataServices();
builder.Services.RegisterApplicationServices();
builder.Services.RegisterServices(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.DashboardMapsterConfiguration();

builder.Services.AddTransient<JwtCookieAuthenticationMiddleware>();

var app = builder.Build();
        
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseItToSeedSqlServer();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
        
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseJwtCookieAuthentication();
app.UseRouting();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseAuthentication();

app.Use(async (context, next) =>
{
    if (context.User.Identity?.IsAuthenticated == true)
    {
        // User is authenticated
        // You can access the authenticated user's information through context.User
    }

    await next.Invoke();
});

app.Run();