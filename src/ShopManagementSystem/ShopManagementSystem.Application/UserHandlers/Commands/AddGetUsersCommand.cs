using MediatR;
using ShopManagementSystem.Application.DTOs;

namespace ShopManagementSystem.Application.UserHandlers.Commands;

public record AddGetUsersCommand : IRequest<List<UserDto>>
{ }