using System.ComponentModel.DataAnnotations;

namespace ShopManagementSystem.Data.Models;

public class ShopProduct
{
    [Key]
    public Guid ShopId { get; set; }
    public Shop Shop { get; set; }

    [Key]
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
}