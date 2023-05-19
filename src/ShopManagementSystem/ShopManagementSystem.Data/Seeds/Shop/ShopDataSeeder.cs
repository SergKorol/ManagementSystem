using ShopManagementSystem.Data.Context;
using ShopManagementSystem.Data.Models;

namespace ShopManagementSystem.Data.Seeds;

public class ShopDataSeeder
{
    public static void SeedShopData(ApplicationDbContext context)
    {
        if (context.Shops.Any())
        {
            return;
        }
        var shops = new List<Shop>
        {
            new Shop { Title = "Cheap smartphones", Phone = "1234567890" },
            new Shop { Title = "Old laptops", Phone = "029389485" },
            new Shop { Title = "Swiss watches", Phone = "493958205" }
        };
        
        context.Shops.AddRange(shops);
    }
}