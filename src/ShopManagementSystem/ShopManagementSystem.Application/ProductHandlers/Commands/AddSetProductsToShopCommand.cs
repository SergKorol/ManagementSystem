using MediatR;

namespace ShopManagementSystem.Application.ProductHandlers.Commands;

public record AddSetProductsToShopCommand : IRequest<bool>
{
    public IEnumerable<string> Ids { get; set; }
    public string ShopId { get; set; }
}