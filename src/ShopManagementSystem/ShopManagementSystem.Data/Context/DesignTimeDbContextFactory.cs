    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;
    
    namespace ShopManagementSystem.Data.Context;
    
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            string? parentDirectory = Path.GetDirectoryName(Path.GetDirectoryName(currentDirectory));
            string path = $"{parentDirectory}/ShopManagementSystem/ShopManagementSystem.Data";
            var configuration = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json")
                .Build();
    
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlite($"DataSource={path}/shop.db;Cache=Shared");
    
            return new ApplicationDbContext(builder.Options);
        }
    }
    
