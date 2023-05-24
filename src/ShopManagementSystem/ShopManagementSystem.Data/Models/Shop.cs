using System.ComponentModel.DataAnnotations;

namespace ShopManagementSystem.Data.Models;

public class Shop
{
    [Key]
    public Guid ShopId { get; set; }

    public string Title { get; set; }
    public string Phone { get; set; }

    public virtual ICollection<ShopProduct> ShopProducts { get; set; }
    public virtual ICollection<Employee> Employees { get; set; }
    
    public Shop()
    {
        ShopProducts = new HashSet<ShopProduct>();
        Employees = new HashSet<Employee>();
    }
}