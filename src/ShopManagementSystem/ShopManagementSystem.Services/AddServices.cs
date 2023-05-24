using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ShopManagementSystem.Application.Dependencies;
using ShopManagementSystem.Data.Context;
using ShopManagementSystem.Services.ProductServices;
using ShopManagementSystem.Services.ShopServices;
using ShopManagementSystem.Services.UserServices;

namespace ShopManagementSystem.Services;

public static class AddServices
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration config)
    {

        services.UseJwtAuthenticationConfig(config);
        services.AddAuthentication();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IShopService, ShopService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ITokenGeneratorService, TokenGeneratorService>();
        services.AddScoped<IDesignTimeDbContextFactory<ApplicationDbContext>, DesignTimeDbContextFactory>();
        services.AddHttpContextAccessor();
        return services;
    }
}