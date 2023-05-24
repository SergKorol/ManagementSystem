using MediatR;
using ShopManagementSystem.Application.Dependencies;
using ShopManagementSystem.Application.ShopHandlers.Commands;

namespace ShopManagementSystem.Application.ShopHandlers;

public sealed class EditShopHandler : IRequestHandler<AddEditShopCommand, bool>
{
    private readonly IShopService _shopService;

    public EditShopHandler(IShopService shopService)
        => _shopService = shopService;
    
    public async Task<bool> Handle(AddEditShopCommand request, CancellationToken cancellationToken)
    {
        return await _shopService.EditShop(request.Id, request.Title, request.Phone);
    }
}