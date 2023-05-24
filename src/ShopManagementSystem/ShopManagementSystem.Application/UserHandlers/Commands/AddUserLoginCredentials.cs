using MediatR;
using ShopManagementSystem.Application.DTOs;

namespace ShopManagementSystem.Application.UserHandlers.Commands;

public record AddUserLoginCredentials : IRequest<string>
{
    public string Login { get; set; }
    public string Password { get; set; }
}