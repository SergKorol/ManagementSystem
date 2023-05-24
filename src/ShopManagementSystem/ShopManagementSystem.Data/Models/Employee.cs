using System.ComponentModel.DataAnnotations;

namespace ShopManagementSystem.Data.Models;

public class Employee
{
    [Key]
    public Guid EmployeeId { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public string Post { get; set; }
    
    public Guid UserId { get; set; }
    public Guid ShopId { get; set; }
    public Shop Shop { get; set; }
}