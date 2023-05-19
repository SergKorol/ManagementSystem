using Microsoft.AspNetCore.Identity;
using ShopManagementSystem.Data.Models;

namespace ShopManagementSystem.Data.Seeds.Identity;

public class IdentityDataSeeder
{

    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    
    public IdentityDataSeeder(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }
    
    public void SeedIdentityData()
    {
        if (_userManager.Users.Any())
        {
            return;
        }

        CreateIdentityRoles().ConfigureAwait(false);

        var admin = new User() { UserName = "admin@shop.com", Email = "admin@shop.com", SecurityStamp = "qwertyui", EmailConfirmed = true, Id = "A16CBD8A-CCD9-40D6-A0AF-3845D5E7B5C2"};
        var user = new User() { UserName = "user@shop.com", Email = "user@shop.com", SecurityStamp = "asdfghjk", EmailConfirmed = true, Id = "CA6246C6-93C0-46CD-88BB-0267384DD840"};
        var employee = new User() { UserName = "employee@shop.com", Email = "employee@shop.com", SecurityStamp = "zxcvbnmm", EmailConfirmed = true, Id = "1A032BFD-8FCC-42EC-8B59-4233B63FB939"};
        CreateIdentityUsers(admin, "p@ssW0rd123!", "Admin").ConfigureAwait(false);
        CreateIdentityUsers(user, "Str0ngP@ssw0rd!", "User").ConfigureAwait(false);
        CreateIdentityUsers(employee, "S3cur3P@ssw0rd!", "Employee").ConfigureAwait(false);
        
    }

    private async Task CreateIdentityRoles()
    {
        var roles = new[]
        {
            new IdentityRole { Name = "Admin" },
            new IdentityRole { Name = "User" },
            new IdentityRole { Name = "Employee" },
        };
        
        foreach (IdentityRole role in roles)
        {
            await _roleManager.CreateAsync(role);
        }
    }

    private async Task CreateIdentityUsers(User user, string password, string role)
    {
       var result = await _userManager.CreateAsync(user, password);

       if (!result.Succeeded)
       {
           throw new Exception("Failed to create user");
       }
       await _userManager.AddToRoleAsync(user, role);
    }
}