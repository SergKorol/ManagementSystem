using MediatR;
using ShopManagementSystem.Application.Dependencies;
using ShopManagementSystem.Application.ProductHandlers.Commands;

namespace ShopManagementSystem.Application.ProductHandlers;

public class DeleteProductFromShop : IRequestHandler<AddDeleteProductFromShop, bool>
{
    private readonly IProductService _productService;

    public DeleteProductFromShop(IProductService productService)
        => _productService = productService;
    
    public Task<bool> Handle(AddDeleteProductFromShop request, CancellationToken cancellationToken)
    {
        return _productService.DeleteProductFromShop(request.ShopId, request.ProductId);
    }
}