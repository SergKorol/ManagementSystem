using MediatR;

namespace ShopManagementSystem.Application.ProductHandlers.Commands;

public record AddCreateProductCommand : IRequest<bool>
{
    public string Name { get; set; }
}