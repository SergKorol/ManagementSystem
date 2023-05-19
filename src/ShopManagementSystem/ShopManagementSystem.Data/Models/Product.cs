using System.ComponentModel.DataAnnotations;

namespace ShopManagementSystem.Data.Models;

public class Product
{
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; }
    
    public ICollection<Shop> Shops { get; set; }
}