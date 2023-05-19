// using System.Reflection;
// using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Configuration;
// using ShopManagementSystem.Data.Models;
//
// namespace ShopManagementSystem.Data.Context;
//
// public class IdentityContext
// {
//     public static ApplicationDbContext CreateDbContext(string[] args)
//     {
//         var configuration = new ConfigurationBuilder()
//             .SetBasePath(Directory.GetCurrentDirectory())
//             .AddJsonFile("appsettings.json")
//             .Build();
//
//         var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
//         var connectionString = configuration.GetConnectionString("DefaultConnection");
//         builder.UseSqlite(connectionString, options => options.MigrationsAssembly("ShopManagementSystem.Data"));
//
//         return new ApplicationDbContext(builder.Options);
//     }
// }