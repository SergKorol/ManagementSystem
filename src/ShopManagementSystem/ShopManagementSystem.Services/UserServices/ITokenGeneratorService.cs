using Microsoft.AspNetCore.Identity;
using ShopManagementSystem.Data.Models;

namespace ShopManagementSystem.Services.UserServices
{
    public interface ITokenGeneratorService
    {
        string GenerateToken(IdentityUser user, IEnumerable<string>? roles = null);
    }
}
