namespace ShopManagementSystem.Application.DTOs;

public record EmployeeDto
{
    public Guid EmployeeId { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public string Post { get; set; }
}