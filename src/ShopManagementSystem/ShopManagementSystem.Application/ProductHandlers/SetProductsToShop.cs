using MediatR;
using ShopManagementSystem.Application.Dependencies;
using ShopManagementSystem.Application.ProductHandlers.Commands;

namespace ShopManagementSystem.Application.ProductHandlers;

public sealed class SetProductsToShop : IRequestHandler<AddSetProductsToShopCommand, bool>
{
    private readonly IProductService _productService;

    public SetProductsToShop(IProductService productService)
        => _productService = productService;
    
    public async Task<bool> Handle(AddSetProductsToShopCommand request, CancellationToken cancellationToken)
    {
        return await _productService.SetSelectedProductsToShop(request.Ids, request.ShopId);
    }
}