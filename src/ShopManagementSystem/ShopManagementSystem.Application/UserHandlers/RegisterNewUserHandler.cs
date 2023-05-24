using MediatR;
using ShopManagementSystem.Application.Dependencies;
using ShopManagementSystem.Application.UserHandlers.Commands;

namespace ShopManagementSystem.Application.UserHandlers;

public sealed class RegisterNewUserHandler : IRequestHandler<AddRegisterNewUserCommand, bool>
{
    private readonly IUserService _userService;
    
    public RegisterNewUserHandler(IUserService  userService)
        => _userService = userService;
    
    public Task<bool> Handle(AddRegisterNewUserCommand request, CancellationToken cancellationToken)
    {
        return _userService.Register(request.Login, request.Password);
    }
}