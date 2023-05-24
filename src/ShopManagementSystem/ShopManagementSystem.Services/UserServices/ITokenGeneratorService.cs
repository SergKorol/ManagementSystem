using Microsoft.AspNetCore.Identity;

namespace ShopManagementSystem.Services.UserServices
{
    public interface ITokenGeneratorService
    {
        string GenerateToken(IdentityUser user, IEnumerable<string>? roles = null);
    }
}
