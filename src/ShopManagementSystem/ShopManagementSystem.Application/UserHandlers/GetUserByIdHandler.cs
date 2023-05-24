using Mapster;
using MediatR;
using ShopManagementSystem.Application.Dependencies;
using ShopManagementSystem.Application.DTOs;
using ShopManagementSystem.Application.UserHandlers.Commands;
using ShopManagementSystem.Data.Models;

namespace ShopManagementSystem.Application.UserHandlers;

public class GetUserByIdHandler : IRequestHandler<AddGetUserByIdCommand, UserDetailDto>
{
    private readonly IUserService _userService;
    
    public GetUserByIdHandler(IUserService  userService)
        => _userService = userService;
    
    public async Task<UserDetailDto> Handle(AddGetUserByIdCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserById(request.Id);
        var dto = user.Adapt<UserDetailDto>();
        return dto;
    }
}