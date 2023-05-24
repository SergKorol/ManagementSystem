using MediatR;
using ShopManagementSystem.Application.Dependencies;
using ShopManagementSystem.Application.ShopHandler.Commands;

namespace ShopManagementSystem.Application.ShopHandler;

public class DeleteShopHandler : IRequestHandler<AddDeleteShopCommand, bool>
{
    private readonly IShopService _shopService;

    public DeleteShopHandler(IShopService shopService)
        => _shopService = shopService;
    
    public Task<bool> Handle(AddDeleteShopCommand request, CancellationToken cancellationToken)
    {
        return _shopService.DeleteShop(request.Id);
    }
}