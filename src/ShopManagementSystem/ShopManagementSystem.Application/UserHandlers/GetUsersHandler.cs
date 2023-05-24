using Mapster;
using MediatR;
using ShopManagementSystem.Application.Dependencies;
using ShopManagementSystem.Application.DTOs;
using ShopManagementSystem.Application.UserHandlers.Commands;
using ShopManagementSystem.Data.Models;

namespace ShopManagementSystem.Application.UserHandlers;

public class GetUsersHandler : IRequestHandler<AddGetUsersCommand, List<UserInfoDto>>
{
    private readonly IUserService _userService;
    
    public GetUsersHandler(IUserService  userService)
        => _userService = userService;
    
    public async Task<List<UserInfoDto>> Handle(AddGetUsersCommand request, CancellationToken cancellationToken)
    {
        var users = await _userService.GetUsers();
        var userInfoDto =  users.Adapt<List<UserInfoDto>>();

        return userInfoDto;
    }
}