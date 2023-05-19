using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ShopManagementSystem.Data.Models;

public class Shop
{
    [Key]
    public Guid Id { get; set; }

    public string Title { get; set; }
    public string Phone { get; set; }

    public ICollection<Product> Products { get; set; }
    public ICollection<Employee> Employees { get; set; }
}