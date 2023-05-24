using MediatR;
using ShopManagementSystem.Application.DTOs;
using ShopManagementSystem.Data.Models;

namespace ShopManagementSystem.Application.UserHandlers.Commands;

public record AddGetUsersCommand : IRequest<List<UserInfoDto>>
{ }