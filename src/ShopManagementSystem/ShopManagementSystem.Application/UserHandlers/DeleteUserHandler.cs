using MediatR;
using ShopManagementSystem.Application.Dependencies;
using ShopManagementSystem.Application.UserHandlers.Commands;

namespace ShopManagementSystem.Application.UserHandlers;

public class DeleteUserHandler : IRequestHandler<AddDeleteUserCommand, bool>
{
    private readonly IUserService _userService;
    
    public DeleteUserHandler(IUserService  userService)
        => _userService = userService;
    
    public async Task<bool> Handle(AddDeleteUserCommand request, CancellationToken cancellationToken)
    {
        return  await _userService.DeleteUser(request.Id);
    }
}