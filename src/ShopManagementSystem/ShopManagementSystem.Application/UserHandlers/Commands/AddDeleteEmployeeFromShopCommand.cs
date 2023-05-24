using MediatR;

namespace ShopManagementSystem.Application.UserHandlers.Commands;

public record AddDeleteEmployeeFromShopCommand : IRequest<bool>
{
    public Guid ShopId { get; set; }
    public Guid EmployeeId { get; set; }
}