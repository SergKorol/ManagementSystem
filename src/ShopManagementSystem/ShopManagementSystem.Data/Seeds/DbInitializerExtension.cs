using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ShopManagementSystem.Data.Context;
using ShopManagementSystem.Data.Seeds.Identity;

namespace ShopManagementSystem.Data.Seeds;

public static class DbInitializerExtension
{
    public static IApplicationBuilder UseItToSeedSqlServer(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app, nameof(app));

        using var scope = app.ApplicationServices.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            var seeder = scope.ServiceProvider.GetRequiredService<IdentityDataSeeder>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<DbInitializer>>();
            DbInitializer.Initialize(context, seeder, logger);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while seeding the database.", ex);
        }

        return app;
    }
}