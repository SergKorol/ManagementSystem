using ShopManagementSystem.Data.Context;
using ShopManagementSystem.Data.Models;

namespace ShopManagementSystem.Data.Seeds;

public class ProductDataSeeder
{
    public static void SeedProductData(ApplicationDbContext context)
    {
        if (context.Products.Any())
        {
            return;
        }
        var products = new List<Product>
        {
            new Product { Id = Guid.Parse("00216165-6E95-4BE9-90F3-7235099940C0"), Name = "Smartphones" },
            new Product { Id = Guid.Parse("5A30F40F-1E4D-47E8-85D5-F92B996E867E"), Name = "Laptops" },
            new Product { Id = Guid.Parse("5AF8580D-143B-4651-A24A-3EE6C9FA8B96"), Name = "Watches" }
        };
        context.Products.AddRange(products);
    }
}