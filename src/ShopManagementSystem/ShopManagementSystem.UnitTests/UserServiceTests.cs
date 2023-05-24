using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using ShopManagementSystem.Application.Dependencies;
using ShopManagementSystem.Data.Models;
using ShopManagementSystem.Services.UserServices;

namespace ShopManagementSystem.UnitTests;

public sealed class UserServiceTests
{
    private readonly UserService _userService;
    private readonly Mock<UserManager<IdentityUser>> _mockUserManager;
    private readonly Mock<RoleManager<IdentityRole>> _mockRoleManager;
    private readonly Mock<SignInManager<IdentityUser>> _mockSignInManager;
    private readonly Mock<IUserClaimsPrincipalFactory<IdentityUser>> _mockClaimsPrincipalFactory;
    private readonly Mock<ITokenGeneratorService> _mockTokenGeneratorService;
    private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
    private readonly Mock<ILogger<UserService>> _mockLogger;
    private readonly Mock<IShopService> _mockShopService;

    public UserServiceTests()
    {
        var userStoreMock = new Mock<IUserStore<IdentityUser>>();
        _mockUserManager =
            new Mock<UserManager<IdentityUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);

        var roleStoreMock = new Mock<IRoleStore<IdentityRole>>();
        _mockRoleManager = new Mock<RoleManager<IdentityRole>>(roleStoreMock.Object, null, null, null, null);

        var contextAccessorMock = new Mock<IHttpContextAccessor>();
        var contextMock = new Mock<HttpContext>();
        contextAccessorMock.Setup(x => x.HttpContext).Returns(contextMock.Object);

        _mockClaimsPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<IdentityUser>>();

        _mockSignInManager = new Mock<SignInManager<IdentityUser>>(_mockUserManager.Object, contextAccessorMock.Object,
            _mockClaimsPrincipalFactory.Object, null, null, null, null);

        _mockTokenGeneratorService = new Mock<ITokenGeneratorService>();
        _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        _mockLogger = new Mock<ILogger<UserService>>();
        _mockShopService = new Mock<IShopService>();

        _userService = new UserService(
            _mockUserManager.Object,
            _mockRoleManager.Object,
            _mockSignInManager.Object,
            _mockTokenGeneratorService.Object,
            _mockHttpContextAccessor.Object,
            _mockLogger.Object,
            _mockShopService.Object
        );
    }


    [Fact]
    public async Task Login_WithValidCredentials_ReturnsToken()
    {
        // Arrange
        var login = "test@test.com";
        var password = "StrongPass123!";
        var user = new IdentityUser { Email = login };
        var principal = new ClaimsPrincipal();
        var token =
            "tuiwa5iWaYVNb=V5w5QeFTXABocygsLQjX0UggOZvT5cKRBPFi!Wc5Ydsq64FcniYJ0jT361/KgeB4Nh30PP0TFuG1IKF3yv!JnrAPJ2v8D!TVDv04/Pf!4ZV?ITHKholGIvGa7JMIVvy-VDCDru4ZTdYIfkFN-GQDSHHQP0qOkj2RV-m2SW-x-uQhqqJ3DcGcYA?io5oKJDzZgRwBrFhtoACb90i?sFijyPwi9HBfteJ/Ng1ilFa4KKx9UaWoy/?kliAV9RCDe8nA5C5CeDDqfBpYHqmhGwnI1haWsTCg/QpgiYB8s34oZhzfVLh3VDw2?!yJcM7xy82Kpjsr19lO3N1sm7Gd5Jf3LB00pqLgnL4PPFm6NwPU?NPrx/kg?l9jnZ5EoPT/8iO1iD-bRwltWTAt!fiw6df2ULD/Bqhruf!=Y6HsTXuJxc8V7?l4FLQZ7/y-EM7=S9!riCOmhfT3LZtI/-xvG5IDXilZw-UIRyZouesV0y=z0UOPpAFvoS";    
        _mockUserManager.Setup(m => m.FindByEmailAsync(login)).ReturnsAsync(user);
        _mockUserManager.Setup(m => m.CheckPasswordAsync(user, password)).ReturnsAsync(true);
        _mockSignInManager.Setup(m => m.CreateUserPrincipalAsync(user)).ReturnsAsync(principal);
        _mockSignInManager.Setup(m => m.PasswordSignInAsync(user, password, true, true))
            .ReturnsAsync(SignInResult.Success);
        _mockTokenGeneratorService.Setup(m => m.GenerateToken(user, null)).Returns(token);
        _mockUserManager.Setup(m => m.GetRolesAsync(user)).ReturnsAsync(new List<string> { "Admin" });

        // Act
        var result = await _userService.Login(login, password);

        // Assert
        Assert.Equal(token, result);
    }

    [Fact]
    public async Task Login_WithInvalidPassword_ReturnsEmptyString()
    {
        // Arrange
        var login = "test@test.com";
        var password = "StrongPass123!";
        var user = new IdentityUser { Email = login };

        _mockUserManager.Setup(m => m.FindByEmailAsync(login)).ReturnsAsync(user);
        _mockUserManager.Setup(m => m.CheckPasswordAsync(user, password)).ReturnsAsync(false);

        // Act
        var result = await _userService.Login(login, password);

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public async Task Register_SuccessfullyRegistersUserAndAddsToRole()
    {
        // Arrange
        var login = "test@test.com";
        var password = "StrongPass123!";
        var newUser = new User { UserName = login, Email = login, EmailConfirmed = true };

        _mockUserManager.Setup(m => m.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);

        _mockUserManager.Setup(m => m.AddToRoleAsync(newUser, "User")).ReturnsAsync(IdentityResult.Success);

        _mockUserManager.Setup(m => m.FindByEmailAsync(login)).ReturnsAsync(newUser);

        _mockRoleManager.Setup(m => m.AddClaimAsync(It.IsAny<IdentityRole>(), It.IsAny<Claim>()))
            .ReturnsAsync(IdentityResult.Success);
        _mockRoleManager.Setup(m => m.UpdateAsync(It.IsAny<IdentityRole>())).ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await _userService.Register(login, password);

        // Assert
        Assert.True(result);
        _mockUserManager.Verify(m => m.FindByEmailAsync(login), Times.Once);
        _mockRoleManager.Verify(m => m.AddClaimAsync(It.IsAny<IdentityRole>(), It.IsAny<Claim>()), Times.Exactly(3));
        _mockRoleManager.Verify(m => m.UpdateAsync(It.IsAny<IdentityRole>()), Times.Once);
    }

    [Fact]
    public async Task GetUsers_ReturnsListOfUsers()
    {
        // Arrange
        var userList = new List<IdentityUser>
        {
            new IdentityUser { Id = "1", UserName = "user1", Email = "user1@user.com" },
            new IdentityUser { Id = "2", UserName = "user2", Email = "user2@user.com" }
        };

        _mockUserManager.Setup(m => m.Users)
            .Returns(userList.AsQueryable());

        // Act
        var result = (await _userService.GetUsers()).ToList();

        // Assert
        Assert.Equal(userList.Count, result.Count);
        Assert.Equal("1", result[0].Id);
        Assert.Equal("user1", result[0].UserName);
        Assert.Equal("user1@user.com", result[0].Email);
        Assert.Equal("2", result[1].Id);
        Assert.Equal("user2", result[1].UserName);
        Assert.Equal("user2@user.com", result[1].Email);
    }

    [Fact]
    public async Task GetUserById_ReturnsUser()
    {
        // Arrange
        var userId = "e0e868cb-c794-421e-9782-c3ac025ed6c9";
        var role = "User";
        var expectedUser = new User
        {
            Id = userId,
            UserName = "John Doe",
            Email = "test@test.com",
            PhoneNumber = "123456789",
            EmailConfirmed = true,
            Role = role
        };
        _mockRoleManager.Setup(m => m.Roles)
            .Returns(new List<IdentityRole> { new IdentityRole { Name = role } }.AsQueryable());

        _mockUserManager.Setup(m => m.GetUsersInRoleAsync(role)).ReturnsAsync(new List<IdentityUser> { expectedUser });

        // Act
        var result = await _userService.GetUserById(userId);
        // Assert
        Assert.Equal(expectedUser.UserName, result.UserName);
        Assert.Equal(expectedUser.Email, result.Email);
        Assert.Equal(expectedUser.PhoneNumber, result.PhoneNumber);
        Assert.Equal(expectedUser.EmailConfirmed, result.EmailConfirmed);
        Assert.Equal(expectedUser.Role, result.Role);
    }

    [Fact]
    public async Task DeleteUser_UserExists_ReturnsTrue()
    {
        // Arrange
        var userId = Guid.NewGuid().ToString();
        var user = new IdentityUser { Id = userId };

        _mockUserManager.Setup(m => m.FindByIdAsync(userId)).ReturnsAsync(user);
        _mockUserManager.Setup(m => m.DeleteAsync(user)).ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await _userService.DeleteUser(userId);

        // Assert
        Assert.True(result);
        _mockUserManager.Verify(m => m.FindByIdAsync(userId), Times.Once);
        _mockUserManager.Verify(m => m.DeleteAsync(user), Times.Once);
    }

    [Fact]
    public async Task SetSelectedUsersToEmployeeInShop_UpdatesUsersAndShop()
    {
        // Arrange
        var selectedUserIds = new List<string> { "user1", "user2" };
        var shopId = Guid.NewGuid();
        var firstName = "John";
        var lastName = "Doe";
        var post = "Employee";

        // Create a list of mock users
        var users = new List<IdentityUser>
        {
            new IdentityUser { Id = "user1" },
            new IdentityUser { Id = "user2" }
        };

        // Mock the UserManager
        _mockUserManager.SetupSequence(m => m.GetUsersInRoleAsync("User"))
            .ReturnsAsync(users.ToList()) // First call returns the list of users
            .ReturnsAsync(new List<IdentityUser>()); // Second call returns an empty list
        _mockUserManager.Setup(m => m.GetUsersInRoleAsync("Employee"))
            .ReturnsAsync(new List<IdentityUser>()); // Return an empty list instead of null
        _mockUserManager.Setup(m => m.GetRolesAsync(It.IsAny<IdentityUser>()))
            .ReturnsAsync(new List<string> { "User" });
        _mockUserManager.Setup(m => m.RemoveFromRoleAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);
        _mockUserManager.Setup(m => m.AddToRoleAsync(It.IsAny<IdentityUser>(), "Employee"))
            .ReturnsAsync(IdentityResult.Success);

        // Mock the ShopService
        var shop = new Shop { ShopId = shopId, Employees = new List<Employee>() };
        var shopService = new Mock<IShopService>();
        shopService.Setup(m => m.GetShopById(shopId.ToString()))
            .ReturnsAsync(shop);
        shopService.Setup(m => m.UpdateShop(It.IsAny<Shop>()))
            .Returns(Task.CompletedTask);

        // Act
        var result =
            await _userService.SetSelectedUsersToEmployeeInShop(selectedUserIds, shopId.ToString(), firstName, lastName,
                post);

        // Assert
        Assert.True(result);
        _mockUserManager.Verify(m => m.GetUsersInRoleAsync("User"), Times.Once);
        _mockUserManager.Verify(m => m.GetRolesAsync(It.IsAny<IdentityUser>()), Times.Exactly(2));
        _mockUserManager.Verify(m => m.RemoveFromRoleAsync(It.IsAny<IdentityUser>(), "User"), Times.Exactly(2));
        _mockUserManager.Verify(m => m.AddToRoleAsync(It.IsAny<IdentityUser>(), "Employee"), Times.Exactly(2));
    }
}