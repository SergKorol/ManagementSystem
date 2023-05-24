namespace ShopManagementSystem.Application.DTOs;

public record UserDto
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
}