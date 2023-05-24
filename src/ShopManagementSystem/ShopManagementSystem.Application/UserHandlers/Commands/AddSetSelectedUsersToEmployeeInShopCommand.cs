using MediatR;

namespace ShopManagementSystem.Application.UserHandlers.Commands;

public record AddSetSelectedUsersToEmployeeInShopCommand : IRequest<bool>
{
    public IEnumerable<string> Ids { get; set; }
    public string ShopId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Post { get; set; }
}