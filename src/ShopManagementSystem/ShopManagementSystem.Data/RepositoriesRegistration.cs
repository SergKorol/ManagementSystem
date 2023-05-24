using Microsoft.Extensions.DependencyInjection;
using ShopManagementSystem.Data.Models;
using ShopManagementSystem.Data.Repository;

namespace ShopManagementSystem.Data;

public static class RepositoriesRegistration
{
    public static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IRepository<Shop>, Repository<Shop>>();
        services.AddScoped<IRepository<Product>, Repository<Product>>();
        services.AddScoped<IRepository<Employee>, Repository<Employee>>();
        services.AddScoped<IRepository<ShopProduct>, Repository<ShopProduct>>();
        return services;
    }
}