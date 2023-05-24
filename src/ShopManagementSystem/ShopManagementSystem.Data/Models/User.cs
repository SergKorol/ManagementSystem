using Microsoft.AspNetCore.Identity;

namespace ShopManagementSystem.Data.Models;

public class User : IdentityUser
{
    public string? Role { get; set; }
}