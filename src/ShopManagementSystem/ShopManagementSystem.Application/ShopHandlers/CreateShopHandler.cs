using MediatR;
using ShopManagementSystem.Application.Dependencies;
using ShopManagementSystem.Application.ShopHandlers.Commands;

namespace ShopManagementSystem.Application.ShopHandlers;

public sealed class CreateShopHandler : IRequestHandler<AddCreateShopCommand, bool>
{
    private readonly IShopService _shopService;

    public CreateShopHandler(IShopService shopService)
        => _shopService = shopService;
    
    public async Task<bool> Handle(AddCreateShopCommand request, CancellationToken cancellationToken)
    {
        return await _shopService.CreateShop(request.Title, request.Phone);
    }
}