using ShopManagementSystem.Data.Context;

namespace ShopManagementSystem.Data.Seeds;

public class SeedService
{
    public static void Seed(ApplicationDbContext context)
    {
        EmployeeDataSeeder.SeedEmployeeData(context);
        ProductDataSeeder.SeedProductData(context);
        ShopDataSeeder.SeedShopData(context);
    }
}