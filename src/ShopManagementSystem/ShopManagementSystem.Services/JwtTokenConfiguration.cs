using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ShopManagementSystem.Services.UserServices;

namespace ShopManagementSystem.Services;

public static class JwtTokenConfiguration
{
    public static IServiceCollection UseJwtAuthenticationConfig(this IServiceCollection services, IConfiguration config)
    {
        var section = config.GetSection(nameof(ApplicationSettings));
        services.Configure<ApplicationSettings>(section);
        var secret = section.GetValue<string>(nameof(ApplicationSettings.Secret));

        if (secret != null)
        {
            var key = Encoding.ASCII.GetBytes(secret);
            services.AddAuthentication();
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
        }
        return services;
    }
}