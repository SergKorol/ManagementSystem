using ShopManagementSystem.Data.Models;

namespace ShopManagementSystem.Application.Dependencies;

public interface IProductService
{
    Task<IEnumerable<Product>> GetProducts();
    Task<bool> CreateProduct(string name);

    Task<Product> GetProductById(string id);
    Task<bool> SetSelectedProductsToShop(IEnumerable<string> selectedProductIds, string shopId);
    Task<bool> EditProduct(Guid id, string name);
    Task<bool> DeleteProduct(Guid id);
    Task<bool> DeleteProductFromShop(Guid requestShopId, Guid requestProductId);
}