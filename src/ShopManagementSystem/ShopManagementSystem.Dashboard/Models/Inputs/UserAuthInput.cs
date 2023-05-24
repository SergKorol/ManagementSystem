namespace ShopManagementSystem.Dashboard.Models.Inputs;

public record UserAuthInput
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}