using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ShopManagementSystem.Application.Configuration;

namespace ShopManagementSystem.Application;

public static class AddApplicationServices
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        services.RegisterMapsterConfiguration();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        return services;
    }
}