using MediatR;

namespace ShopManagementSystem.Application.UserHandlers.Commands;

public record AddUserLoginCredentials : IRequest<string>
{
    public string Login { get; set; }
    public string Password { get; set; }
}