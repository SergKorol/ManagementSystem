using MediatR;

namespace ShopManagementSystem.Application.ProductHandlers.Commands;

public record AddDeleteProductFromShop :IRequest<bool>
{
    public Guid ShopId { get; set; }
    public Guid ProductId { get; set; }
}