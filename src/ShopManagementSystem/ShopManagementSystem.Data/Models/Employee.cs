using System.ComponentModel.DataAnnotations;

namespace ShopManagementSystem.Data.Models;

public class Employee
{
    [Key]
    public Guid Id { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public string Post { get; set; }
    
    public Guid UserId { get; set; }
}