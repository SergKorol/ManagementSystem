using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopManagementSystem.Data.Context;
using ShopManagementSystem.Data.Models;
using ShopManagementSystem.Data.Repository;
using ShopManagementSystem.Data.Seeds;
using ShopManagementSystem.Data.Seeds.Identity;

namespace ShopManagementSystem.Data;

public static class AddDataServices
{
    public static IServiceCollection RegisterDataServices(this IServiceCollection services)
    {
        // var currentDirectory = Directory.GetCurrentDirectory();
        // if (currentDirectory != "/app")
        // {
        //     
        // }
        // string? parentDirectory = Path.GetDirectoryName(Path.GetDirectoryName(currentDirectory));
        // string path = $"{parentDirectory}/ShopManagementSystem/ShopManagementSystem.Data";
        string path;
        string currentDirectory = Directory.GetCurrentDirectory();
        if (currentDirectory != "/app")
        {
            string? parentDirectory = Path.GetDirectoryName(Path.GetDirectoryName(currentDirectory));
            path = $"{parentDirectory}/ShopManagementSystem/ShopManagementSystem.Data";
        }
        else
        {
            path = "/app/src/ShopManagementSystem/ShopManagementSystem.Data";
        }
        var configuration = new ConfigurationBuilder()
            .SetBasePath(path)
            .AddJsonFile("appsettings.json")
            .Build();
        services.AddDbContext<ApplicationDbContext>(
            options =>
                options.UseSqlite(
                    $"DataSource={path}/shop.db;Cache=Shared",
                    x => x.MigrationsAssembly("ShopManagementSystem.Data")));
        
        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        
        services.AddScoped<DbInitializer>();
        services.AddScoped<IdentityDataSeeder>();
        services.AddScoped<UserManager<IdentityUser>>();
        services.AddScoped<RoleManager<IdentityRole>>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IRepository<Shop>, Repository<Shop>>();
        services.AddScoped<IRepository<Product>, Repository<Product>>();
        services.AddScoped<IRepository<Employee>, Repository<Employee>>();
        services.AddScoped<IRepository<ShopProduct>, Repository<ShopProduct>>();
        services.AddDatabaseDeveloperPageExceptionFilter();

        return services;
    }
}