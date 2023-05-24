using Mapster;
using MediatR;
using ShopManagementSystem.Application.Dependencies;
using ShopManagementSystem.Application.DTOs;
using ShopManagementSystem.Application.ProductHandlers.Commands;

namespace ShopManagementSystem.Application.ProductHandlers;

public sealed class GetProductByIdHandler : IRequestHandler<AddGetProductByIdCommand, ProductDto>
{
    private readonly IProductService _productService;

    public GetProductByIdHandler(IProductService productService)
        => _productService = productService;
    
    public async Task<ProductDto> Handle(AddGetProductByIdCommand request, CancellationToken cancellationToken)
    {
        var product = await _productService.GetProductById(request.Id);
        var dto = product.Adapt<ProductDto>();
        return dto;
    }
}