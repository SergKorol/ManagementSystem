using MediatR;
using ShopManagementSystem.Application.DTOs;

namespace ShopManagementSystem.Application.ShopHandler.Commands;

public record AddGetShopsCommand : IRequest<IEnumerable<ShopDto>> { }