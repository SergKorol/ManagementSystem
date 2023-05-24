using MediatR;
using ShopManagementSystem.Application.Dependencies;
using ShopManagementSystem.Application.UserHandlers.Commands;

namespace ShopManagementSystem.Application.UserHandlers;

public sealed class SetSelectedUsersToEmployeeHandler : IRequestHandler<AddSetSelectedUsersToEmployeeInShopCommand, bool>
{
    private readonly IUserService _userService;
    
    public SetSelectedUsersToEmployeeHandler(IUserService  userService)
        => _userService = userService;
    public Task<bool> Handle(AddSetSelectedUsersToEmployeeInShopCommand request, CancellationToken cancellationToken)
    {
        return _userService.SetSelectedUsersToEmployeeInShop(request.Ids, request.ShopId, request.FirstName, request.LastName, request.Post);
    }
}