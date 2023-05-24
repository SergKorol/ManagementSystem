using MediatR;

namespace ShopManagementSystem.Application.ShopHandlers.Commands;

public record AddDeleteShopCommand : IRequest<bool>
{
    public Guid Id { get; set; }   
}