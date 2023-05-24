using Mapster;
using MediatR;
using ShopManagementSystem.Application.Dependencies;
using ShopManagementSystem.Application.DTOs;
using ShopManagementSystem.Application.UserHandlers.Commands;

namespace ShopManagementSystem.Application.UserHandlers;

public sealed class GetUsersHandler : IRequestHandler<AddGetUsersCommand, List<UserDto>>
{
    private readonly IUserService _userService;
    
    public GetUsersHandler(IUserService  userService)
        => _userService = userService;
    
    public async Task<List<UserDto>> Handle(AddGetUsersCommand request, CancellationToken cancellationToken)
    {
        var users = await _userService.GetUsers();
        var userInfoDto =  users.Adapt<List<UserDto>>();

        return userInfoDto;
    }
}