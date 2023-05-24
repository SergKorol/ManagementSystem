using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ShopManagementSystem.Data.Models;

namespace ShopManagementSystem.Application.Dependencies;

public interface IUserService
{
    Task<string> Login(string login, string password);
    Task<bool> Register(string login, string password);
    Task<IEnumerable<User>> GetUsers();
    Task<User> GetUserById(string userId);
    Task<bool> DeleteUser(string id);
    Task<HttpContext> VerifyToken(HttpContext httpContext, string token);
    Task<bool> SetSelectedUsersToEmployeeInShop(IEnumerable<string> selectedUserIds, string shopId, string firstName, string lastName, string post);
    Task<bool> DeleteEmployeeFromShop(Guid employeeId, Guid shopId);
}