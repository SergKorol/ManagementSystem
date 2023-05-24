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
        var section = config.GetSection(nameof(ApplicationSettings));
        services.Configure<ApplicationSettings>(section);
        var secret = section.GetValue<string>(nameof(ApplicationSettings.Secret));
        
        var key = Encoding.ASCII.GetBytes(secret);
        
        services.AddAuthentication(authentication =>
            {
                authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                authentication.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(bearer =>
            {
                bearer.RequireHttpsMetadata = false;
                bearer.SaveToken = true;
                bearer.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = config["Jwt:Issuer"],
                    ValidAudience = config["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };
            }).AddCookie();
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