using MediatR;
using ShopManagementSystem.Application.Dependencies;
using ShopManagementSystem.Application.ShopHandlers.Commands;

namespace ShopManagementSystem.Application.ShopHandlers;

public sealed class DeleteShopHandler : IRequestHandler<AddDeleteShopCommand, bool>
{
    private readonly IShopService _shopService;

    public DeleteShopHandler(IShopService shopService)
        => _shopService = shopService;
    
    public Task<bool> Handle(AddDeleteShopCommand request, CancellationToken cancellationToken)
    {
        return _shopService.DeleteShop(request.Id);
    }
}