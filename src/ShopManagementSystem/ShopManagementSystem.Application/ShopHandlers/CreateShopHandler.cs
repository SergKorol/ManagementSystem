using MediatR;
using ShopManagementSystem.Application.Dependencies;
using ShopManagementSystem.Application.ShopHandler.Commands;
using ShopManagementSystem.Application.ShopHandlers.Commands;

namespace ShopManagementSystem.Application.ShopHandler;

public class CreateShopHandler : IRequestHandler<AddCreateShopCommand, bool>
{
    private readonly IShopService _shopService;

    public CreateShopHandler(IShopService shopService)
        => _shopService = shopService;
    
    public async Task<bool> Handle(AddCreateShopCommand request, CancellationToken cancellationToken)
    {
        return await _shopService.CreateShop(request.Title, request.Phone);
    }
}