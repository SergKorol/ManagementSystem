using MediatR;

namespace ShopManagementSystem.Application.ProductHandlers.Commands;

public record AddEditProductCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}