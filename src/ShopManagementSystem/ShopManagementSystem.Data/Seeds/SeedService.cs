using ShopManagementSystem.Data.Context;

namespace ShopManagementSystem.Data.Seeds;

public sealed class SeedService
{
    public static void Seed(ApplicationDbContext context)
    {
        EmployeeDataSeeder.SeedEmployeeData(context);
        ProductDataSeeder.SeedProductData(context);
        ShopDataSeeder.SeedShopData(context);
        ShopProductDataSeeder.SeedShopProductData(context);
    }
}