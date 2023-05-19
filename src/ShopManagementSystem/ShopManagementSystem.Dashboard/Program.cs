using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopManagementSystem.Dashboard;
using ShopManagementSystem.Data;
using ShopManagementSystem.Data.Context;
using ShopManagementSystem.Data.Seeds;
using ShopManagementSystem.Data.Seeds.Identity;

// public static class Program
// {
//     public static void Main(string[] args)
//     {
//         var builder = WebApplication.CreateBuilder(args);
//
//
// // Add services to the container.
//         builder.Services.AddControllersWithViews();
//         builder.Services.RegisterDataServices();
//
//         var app = builder.Build();
//
//
//
//         if (app.Environment.IsDevelopment())
//         {
//             app.UseMigrationsEndPoint();
//         }
//         else
//         {
//             app.UseExceptionHandler("/Home/Error");
//             app.UseHsts();
//         }
//
//         app.UseHttpsRedirection();
//         app.UseStaticFiles();
//
//         app.UseRouting();
//
//         app.UseAuthorization();
//
//         app.MapControllerRoute(
//             name: "default",
//             pattern: "{controller=Home}/{action=Index}/{id?}");
//
//         app.Run();
//     }
// }

public class Program
{
    public static void Main(string[] args)
    {
        
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        var currentDirectory = Directory.GetCurrentDirectory();
        string? parentDirectory = Path.GetDirectoryName(Path.GetDirectoryName(currentDirectory));
        string path = $"{parentDirectory}/ShopManagementSystem/ShopManagementSystem.Data";
        var configuration = new ConfigurationBuilder()
            .SetBasePath(path)
            .AddJsonFile("appsettings.json")
            .Build();
        builder.Services.AddDbContext<ApplicationDbContext>(
            options =>
                options.UseSqlite(
                    $"DataSource={path}/shop.db;Cache=Shared",
                    x => x.MigrationsAssembly("ShopManagementSystem.Data")));
        
        builder.Services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        builder.Services.RegisterDataServices();
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

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}