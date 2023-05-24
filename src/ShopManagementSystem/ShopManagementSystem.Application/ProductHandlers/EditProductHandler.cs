using MediatR;
using ShopManagementSystem.Application.Dependencies;
using ShopManagementSystem.Application.ProductHandlers.Commands;

namespace ShopManagementSystem.Application.ProductHandlers;

public class EditProductHandler : IRequestHandler<AddEditProductCommand, bool>
{
    private readonly IProductService _productService;

    public EditProductHandler(IProductService productService)
        => _productService = productService;

    
    public async Task<bool> Handle(AddEditProductCommand request, CancellationToken cancellationToken)
    {
        return await _productService.EditProduct(request.Id, request.Name);
    }
}