using MediatR;
using ShopManagementSystem.Application.Dependencies;
using ShopManagementSystem.Application.ProductHandlers.Commands;

namespace ShopManagementSystem.Application.ProductHandlers;

public class DeleteProductHandler : IRequestHandler<AddDeleteProductCommand, bool>
{
    private readonly IProductService _productService;

    public DeleteProductHandler(IProductService productService)
        => _productService = productService;
    
    public async Task<bool> Handle(AddDeleteProductCommand request, CancellationToken cancellationToken)
    {
        return await _productService.DeleteProduct(request.Id);
    }
}