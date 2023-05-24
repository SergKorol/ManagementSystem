using Mapster;
using MediatR;
using ShopManagementSystem.Application.Dependencies;
using ShopManagementSystem.Application.DTOs;
using ShopManagementSystem.Application.ShopHandler.Commands;

namespace ShopManagementSystem.Application.ShopHandler;

public class GetShopsHandler : IRequestHandler<AddGetShopsCommand, IEnumerable<ShopDto>>
{
    private readonly IShopService _shopService;

    public GetShopsHandler(IShopService shopService)
        => _shopService = shopService;
    
    public async Task<IEnumerable<ShopDto>> Handle(AddGetShopsCommand request, CancellationToken cancellationToken)
    {
        var shops = await _shopService.GetShops();
        var dtos = shops.Adapt<IEnumerable<ShopDto>>();
        
        return dtos;
    }
}