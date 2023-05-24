using MediatR;

namespace ShopManagementSystem.Application.ShopHandler.Commands;

public record AddDeleteShopCommand : IRequest<bool>
{
    public Guid Id { get; set; }   
}