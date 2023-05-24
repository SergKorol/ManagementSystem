namespace ShopManagementSystem.Application.DTOs;

public record ProductDto
{
    public Guid ProductId { get; set; }

    public string Name { get; set; }
}