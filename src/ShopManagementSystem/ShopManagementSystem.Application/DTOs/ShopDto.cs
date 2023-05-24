using ShopManagementSystem.Data.Models;

namespace ShopManagementSystem.Application.DTOs;

public record ShopDto
{
    public Guid ShopId { get; set; }

    public string Title { get; set; }
    public string Phone { get; set; }
    public IEnumerable<ProductDto> Products { get; set; }
    public IEnumerable<EmployeeDto> Employees { get; set; }
}