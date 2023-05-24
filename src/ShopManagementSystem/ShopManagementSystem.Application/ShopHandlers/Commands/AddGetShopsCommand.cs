using MediatR;
using ShopManagementSystem.Application.DTOs;

namespace ShopManagementSystem.Application.ShopHandlers.Commands;

public record AddGetShopsCommand : IRequest<IEnumerable<ShopDto>> { }