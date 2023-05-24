using Mapster;
using MediatR;
using ShopManagementSystem.Application.Dependencies;
using ShopManagementSystem.Application.DTOs;
using ShopManagementSystem.Application.ShopHandler.Commands;

namespace ShopManagementSystem.Application.ShopHandler;

public class GetShopByIdHandler : IRequestHandler<AddGetShopByIdCommand, ShopDto>
{
    private readonly IShopService _shopService;

    public GetShopByIdHandler(IShopService shopService)
        => _shopService = shopService;
    
    public async Task<ShopDto> Handle(AddGetShopByIdCommand request, CancellationToken cancellationToken)
    {
        var shop = await _shopService.GetShopById(request.Id);
        var dto = shop?.Adapt<ShopDto>();
        if (dto != null) return dto;
        return null!;
    }
    
    
}