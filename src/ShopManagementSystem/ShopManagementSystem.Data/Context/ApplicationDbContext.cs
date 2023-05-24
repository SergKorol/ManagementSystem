using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopManagementSystem.Data.Models;

namespace ShopManagementSystem.Data.Context;

public sealed class ApplicationDbContext : IdentityDbContext
{

    public DbSet<Shop> Shops { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<ShopProduct> ShopProducts { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        builder.Entity<ShopProduct>()
            .HasKey(sp => new { sp.ShopId, sp.ProductId });

        builder.Entity<ShopProduct>()
            .HasOne(sp => sp.Shop)
            .WithMany(s => s.ShopProducts)
            .HasForeignKey(sp => sp.ShopId);

        builder.Entity<ShopProduct>()
            .HasOne(sp => sp.Product)
            .WithMany(p => p.ShopProducts)
            .HasForeignKey(sp => sp.ProductId);

        base.OnModelCreating(builder);
    }
}