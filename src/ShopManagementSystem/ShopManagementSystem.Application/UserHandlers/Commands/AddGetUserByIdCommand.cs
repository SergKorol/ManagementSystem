using MediatR;
using ShopManagementSystem.Application.DTOs;
using ShopManagementSystem.Data.Models;

namespace ShopManagementSystem.Application.UserHandlers.Commands;

public record AddGetUserByIdCommand : IRequest<UserDetailDto>
{
    public string Id { get; set; }
}