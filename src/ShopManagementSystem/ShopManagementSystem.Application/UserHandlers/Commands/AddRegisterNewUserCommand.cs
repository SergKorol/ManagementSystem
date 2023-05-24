using MediatR;

namespace ShopManagementSystem.Application.UserHandlers.Commands;

public record AddRegisterNewUserCommand : IRequest<bool>
{
    public string Login { get; set; }
    public string Password { get; set; }
}