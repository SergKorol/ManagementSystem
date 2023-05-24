using Microsoft.Extensions.Logging;
using ShopManagementSystem.Data.Context;
using ShopManagementSystem.Data.Seeds.Identity;

namespace ShopManagementSystem.Data.Seeds;

internal sealed class DbInitializer
{
    internal static void Initialize(ApplicationDbContext dbContext, IdentityDataSeeder identitySeeder, ILogger<DbInitializer> logger)
    {
        ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));
        try
        {
            dbContext.Database.EnsureCreated();
            identitySeeder.SeedIdentityData();
            SeedService.Seed(dbContext);
            dbContext.SaveChanges();
        }
        catch (Exception e)
        {
            logger.LogError("The seed data failed ");
        }
    }
}