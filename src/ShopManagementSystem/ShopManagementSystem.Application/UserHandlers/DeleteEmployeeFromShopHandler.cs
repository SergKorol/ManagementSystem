using MediatR;
using ShopManagementSystem.Application.Dependencies;
using ShopManagementSystem.Application.UserHandlers.Commands;

namespace ShopManagementSystem.Application.UserHandlers;

public class DeleteEmployeeFromShopHandler : IRequestHandler<AddDeleteEmployeeFromShopCommand, bool>
{
    private readonly IUserService _userService;
    
    public DeleteEmployeeFromShopHandler(IUserService  userService)
        => _userService = userService;
    
    public Task<bool> Handle(AddDeleteEmployeeFromShopCommand request, CancellationToken cancellationToken)
    {
        return  _userService.DeleteEmployeeFromShop(request.EmployeeId, request.ShopId);
    }
}