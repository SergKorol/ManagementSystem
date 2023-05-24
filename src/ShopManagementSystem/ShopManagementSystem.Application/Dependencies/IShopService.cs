using ShopManagementSystem.Data.Models;

namespace ShopManagementSystem.Application.Dependencies;

public interface IShopService
{
    Task<IEnumerable<Shop>> GetShops();
    Task<bool> CreateShop(string title, string phone);
    Task<bool> EditShop(Guid id, string title, string phone);
    Task<Shop> GetShopById(string id);
    Task<bool> DeleteShop(Guid id);
    Task UpdateShop(Shop shop);
}