using MediatR;
using ShopManagementSystem.Application.DTOs;

namespace ShopManagementSystem.Application.ShopHandler.Commands;

public record AddGetShopByIdCommand : IRequest<ShopDto>
{
    public string Id { get; set; }
}