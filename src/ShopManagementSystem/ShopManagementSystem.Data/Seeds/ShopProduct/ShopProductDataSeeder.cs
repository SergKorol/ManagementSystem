using ShopManagementSystem.Data.Context;
using ShopManagementSystem.Data.Models;

namespace ShopManagementSystem.Data.Seeds;

public static class ShopProductDataSeeder
{
    public static void SeedShopProductData(ApplicationDbContext context)
    {
        if (context.ShopProducts.Any())
        {
            return;
        }

        var shopProducts = new ShopProduct
        {
            ShopId = Guid.Parse("4F8B0BED-DF39-47C9-9A6D-87BD6917CDAE"),
            ProductId = Guid.Parse("00216165-6E95-4BE9-90F3-7235099940C0")
        };
        
        context.ShopProducts.Add(shopProducts);
    }
}