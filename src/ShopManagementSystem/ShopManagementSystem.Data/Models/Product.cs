using System.ComponentModel.DataAnnotations;

namespace ShopManagementSystem.Data.Models;

public class Product
{
    [Key]
    public Guid ProductId { get; set; }

    public string Name { get; set; }
    
    public virtual ICollection<ShopProduct> ShopProducts { get; set; }
}