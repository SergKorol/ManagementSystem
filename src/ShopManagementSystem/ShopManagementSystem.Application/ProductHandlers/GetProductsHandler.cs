using Mapster;
using MediatR;
using ShopManagementSystem.Application.Dependencies;
using ShopManagementSystem.Application.DTOs;
using ShopManagementSystem.Application.ProductHandlers.Commands;

namespace ShopManagementSystem.Application.ProductHandlers;

public class GetProductsHandler : IRequestHandler<AddGetProductsCommand, IEnumerable<ProductDto>>
{
    private readonly IProductService _productService;

    public GetProductsHandler(IProductService productService)
        => _productService = productService;
    
    public async Task<IEnumerable<ProductDto>> Handle(AddGetProductsCommand request, CancellationToken cancellationToken)
    {
        var products = await _productService.GetProducts();
        var dtos = products.Adapt<IEnumerable<ProductDto>>();
        return dtos;
    }
}