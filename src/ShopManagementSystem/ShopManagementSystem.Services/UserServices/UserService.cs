using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ShopManagementSystem.Application.Dependencies;
using ShopManagementSystem.Data.Models;

namespace ShopManagementSystem.Services.UserServices;

public sealed class UserService : IUserService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly ITokenGeneratorService _tokenGeneratorService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<UserService> _logger;
    private readonly IShopService _shopService;

    public UserService(UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        SignInManager<IdentityUser> signInManager,
        ITokenGeneratorService tokenGeneratorService,
        IHttpContextAccessor httpContextAccessor,
        ILogger<UserService> logger,
        IShopService shopService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _tokenGeneratorService = tokenGeneratorService;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
        _shopService = shopService;
    }
      
    public async Task<string> Login(string login, string password)
    {
        var user = await _userManager.FindByEmailAsync(login);
        if (user == null)
        {
            _logger.LogError("User with email {0} not found", login);
            return string.Empty;
        }
        
        var passwordValid = await _userManager.CheckPasswordAsync(user, password);
        if (!passwordValid)
        {
            _logger.LogError("Password for user {0} is incorrect", login);
            return string.Empty;
        }
        
        var token = _tokenGeneratorService.GenerateToken(user);
        var principal = await _signInManager.CreateUserPrincipalAsync(user);
        
        var result = await _signInManager.PasswordSignInAsync(user, password, true, true);
        if (result.Succeeded)
        {
            var identityUser = _userManager.GetUserAsync(principal).GetAwaiter().GetResult();
            if (identityUser != null)
                await _userManager.SetAuthenticationTokenAsync(user, "Email", "Authentication", token);

            var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault() ?? string.Empty;
            if (user.Email != null)
            {
                var claims = new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, user.Id),
                    new(ClaimTypes.Name, user.Email),
                    new(ClaimTypes.Role, role)
                };
                var identityRole = new IdentityRole(role);
                await _userManager.AddClaimsAsync(user, claims);
                claims.ForEach(async claim => await _roleManager.AddClaimAsync( identityRole, claim));
                await _roleManager.UpdateAsync(identityRole);
            }
        }

        if (_httpContextAccessor.HttpContext != null) _httpContextAccessor.HttpContext.User = principal;
        
        return token;
    }

    public async Task<bool> Register(string login, string password)
    {
        var newUser = new User() { UserName = login, Email = login, EmailConfirmed = true};
        var result = await _userManager.CreateAsync(newUser, password);
        
        if (!result.Succeeded)
        {
            _logger.LogError("Failed to register user {0}", login);
            return false;
        }
        await _userManager.AddToRoleAsync(newUser, "User");
        
        var user = await _userManager.FindByEmailAsync(login);
        if (user == null)
        {
            _logger.LogError("User with email {0} not found", login);
            return false;
        }
        
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Name, user.Email ?? string.Empty),
            new(ClaimTypes.Role, "User")
        };
        var identityRole = new IdentityRole("User");
        claims.ForEach(async claim => await _roleManager.AddClaimAsync( new IdentityRole("User"), claim));
        await _roleManager.UpdateAsync(identityRole);
        return result.Succeeded;
    }

    public async Task<IEnumerable<User>> GetUsers()
    {
        var users =  _userManager.Users.Select(iu => new User
            {
                Id = iu.Id,
                UserName = iu.UserName,
                Email = iu.Email
            }).AsEnumerable();
        
        return users;
    }

    public async Task<User> GetUserById(string userId)
    {
        var roles = _roleManager.Roles.Select(n => n.Name).ToList();
        foreach (var role in roles)
        {
            var t = await _userManager.GetUsersInRoleAsync(role ?? string.Empty);
            var user = (await _userManager.GetUsersInRoleAsync(role ?? string.Empty)).Where(x => x.Id == userId)
                .Select(x => new User
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    EmailConfirmed = x.EmailConfirmed
                }).FirstOrDefault();
            if (user != null)
            {
                user.Role = role;
                return user;
            }
        }
        return new User();
    }

    public async Task<bool> DeleteUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user != null)
        {
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        return false;
    }

    public async Task<HttpContext> VerifyToken(HttpContext httpContext, string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(token);
        var loginClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "unique_name");
        if (loginClaim != null)
        {
            string login = loginClaim.Value;
            var user = await _userManager.FindByEmailAsync(login);
            if (user == null) return new DefaultHttpContext();
            var principal = await _signInManager.CreateUserPrincipalAsync(user);
            httpContext.User = principal;
            return  httpContext;
        }

        return new DefaultHttpContext();
    }

    public async Task<bool> SetSelectedUsersToEmployeeInShop(IEnumerable<string> selectedUserIds, string shopId, string firstName, string lastName, string post)
    {
        try
        {
            var users = (await _userManager.GetUsersInRoleAsync("User")).Where(x => selectedUserIds.Contains(x.Id));
        
            var shop = await  _shopService.GetShopById(shopId);
            foreach (var user in users)
            {
                var currentRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                await _userManager.RemoveFromRoleAsync(user, currentRole);
                await _userManager.AddToRoleAsync(user, "Employee");
            }

            var employees = (await _userManager.GetUsersInRoleAsync("Employee")).Where(x => selectedUserIds.Contains(x.Id))
                .Select(x => new Employee
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Post = post,
                    UserId = Guid.Parse(x.Id)
                });
            foreach (var employee in employees)
            {
                shop.Employees.Add(employee);
            }
            await _shopService.UpdateShop(shop);
        
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return false;
        }
    }

    public async Task<bool> DeleteEmployeeFromShop(Guid employeeId, Guid shopId)
    {
        try
        {
            var shop = await  _shopService.GetShopById(shopId.ToString());
            var employee = shop.Employees.FirstOrDefault(x => x.EmployeeId == employeeId);
            if (employee != null)
            {
                shop.Employees.Remove(employee);
            }

            var user = (await _userManager.GetUsersInRoleAsync("Employee")).FirstOrDefault(x =>
                Guid.Parse(x.Id) == employee.UserId);
            if(user != null)
            {
                var currentRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                await _userManager.RemoveFromRoleAsync(user, currentRole);
                await _userManager.AddToRoleAsync(user, "User");
            }
        
            await _shopService.UpdateShop(shop);

            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return false;
        }
    }
}