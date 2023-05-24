using MediatR;
using ShopManagementSystem.Application.DTOs;

namespace ShopManagementSystem.Application.ProductHandlers.Commands;

public record AddGetProductsCommand : IRequest<IEnumerable<ProductDto>> { }