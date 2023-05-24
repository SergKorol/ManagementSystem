using MediatR;
using ShopManagementSystem.Application.Dependencies;
using ShopManagementSystem.Application.ProductHandlers.Commands;

namespace ShopManagementSystem.Application.ProductHandlers;

public sealed class CreateProductHandler : IRequestHandler<AddCreateProductCommand, bool>
{
    private readonly IProductService _productService;

    public CreateProductHandler(IProductService productService)
        => _productService = productService;
    
    public async Task<bool> Handle(AddCreateProductCommand request, CancellationToken cancellationToken)
    {
        return await _productService.CreateProduct(request.Name);
    }
}