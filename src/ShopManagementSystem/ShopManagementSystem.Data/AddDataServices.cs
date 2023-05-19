using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShopManagementSystem.Data.Context;
using ShopManagementSystem.Data.Models;
using ShopManagementSystem.Data.Seeds;
using ShopManagementSystem.Data.Seeds.Identity;

namespace ShopManagementSystem.Data;

public static class AddDataServices
{
    public static IServiceCollection RegisterDataServices(this IServiceCollection serviceCollection)
    {
        // var currentDirectory = Directory.GetCurrentDirectory();
        // string? parentDirectory = Path.GetDirectoryName(Path.GetDirectoryName(currentDirectory));
        // string path = $"{parentDirectory}/ShopManagementSystem/ShopManagementSystem.Data";
        // var configuration = new ConfigurationBuilder()
        //     .SetBasePath(path)
        //     .AddJsonFile("appsettings.json")
        //     .Build();
        
        
        
        
        serviceCollection.AddScoped<DbInitializer>();
        serviceCollection.AddScoped<IdentityDataSeeder>();
        serviceCollection.AddScoped<UserManager<IdentityUser>>();
        serviceCollection.AddScoped<RoleManager<IdentityRole>>();
        // serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
        // serviceCollection.AddSingleton<IdentityDataSeeder>();
        // serviceCollection.AddDatabaseDeveloperPageExceptionFilter();

        return serviceCollection;
    }

    // public static IServiceCollection AddDatabase<TDbContext>(this IServiceCollection services)
    //     where TDbContext : DbContext
    // {
    //     var configuration = new ConfigurationBuilder()
    //         .SetBasePath(Directory.GetCurrentDirectory())
    //         .AddJsonFile("appsettings.json")
    //         .Build();
    //     services
    //         .AddScoped<DbContext, TDbContext>()
    //         .AddDbContextPool<TDbContext>(options =>
    //             options.UseSqlite(
    //                 configuration.GetConnectionString("DefaultConnection")));
    //
    //     return services;
    // }


}