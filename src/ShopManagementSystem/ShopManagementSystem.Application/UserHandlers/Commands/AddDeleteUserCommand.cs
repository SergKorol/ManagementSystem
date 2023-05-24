using MediatR;

namespace ShopManagementSystem.Application.UserHandlers.Commands;

public record AddDeleteUserCommand : IRequest<bool>
{
        public string Id { get; set; }
}