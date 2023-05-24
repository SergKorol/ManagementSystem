using ShopManagementSystem.Data.Context;
using ShopManagementSystem.Data.Models;

namespace ShopManagementSystem.Data.Seeds;

public sealed class ShopDataSeeder
{
    public static void SeedShopData(ApplicationDbContext context)
    {
        if (context.Shops.Any())
        {
            return;
        }
        
        var shops = new List<Shop>
        {
            new() { Title = "Cheap smartphones", Phone = "1234567890", ShopId = Guid.Parse("4F8B0BED-DF39-47C9-9A6D-87BD6917CDAE")},
            new() { Title = "Old laptops", Phone = "029389485", ShopId = Guid.Parse("A054F991-991B-4FBC-A5E8-2BF1BA57085B") },
            new() { Title = "Swiss watches", Phone = "493958205", ShopId = Guid.Parse("8509B8AC-5F76-46C1-90AD-661E11C6EEE6") }
        };
        
        context.Shops.AddRange(shops);
    }
}