using MediatR;

namespace ShopManagementSystem.Application.ShopHandler.Commands;

public record AddEditShopCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Phone { get; set; }
}