using MediatR;
using ShopManagementSystem.Application.Dependencies;
using ShopManagementSystem.Application.UserHandlers.Commands;

namespace ShopManagementSystem.Application.UserHandlers;

public class UserLoginHandler : IRequestHandler<AddUserLoginCredentials, string>
{
    private readonly IUserService _userService;
    
    public UserLoginHandler(IUserService  userService)
        => _userService = userService;
    
    
    public async Task<string> Handle(AddUserLoginCredentials request, CancellationToken cancellationToken)
    {
        var token = await _userService.Login(request.Login, request.Password);
        return token;
    }
}