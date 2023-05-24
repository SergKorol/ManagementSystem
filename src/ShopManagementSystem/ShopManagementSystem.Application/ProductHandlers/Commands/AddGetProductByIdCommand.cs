using MediatR;
using ShopManagementSystem.Application.DTOs;

namespace ShopManagementSystem.Application.ProductHandlers.Commands;

public record AddGetProductByIdCommand : IRequest<ProductDto>
{
    public string Id { get; set; }
}