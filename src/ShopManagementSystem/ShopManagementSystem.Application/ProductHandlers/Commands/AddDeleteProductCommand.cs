using MediatR;

namespace ShopManagementSystem.Application.ProductHandlers.Commands;

public record AddDeleteProductCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}