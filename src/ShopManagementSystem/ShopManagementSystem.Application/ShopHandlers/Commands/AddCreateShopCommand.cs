using MediatR;

namespace ShopManagementSystem.Application.ShopHandlers.Commands;

public record AddCreateShopCommand : IRequest<bool>
{
    public string Title { get; set; }
    public string Phone { get; set; }
}