using MediatR;
using ShopManagementSystem.Application.DTOs;

namespace ShopManagementSystem.Application.UserHandlers.Commands;

public record AddGetUserByIdCommand : IRequest<UserDetailDto>
{
    public string Id { get; set; }
}