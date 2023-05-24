using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;
using ShopManagementSystem.Application.Dependencies;
using ShopManagementSystem.Data;
using ShopManagementSystem.Data.Context;
using ShopManagementSystem.Data.Models;

namespace ShopManagementSystem.Services.ProductServices;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private ILogger<ProductService> _logger;

    public ProductService(IUnitOfWork unitOfWork, ILogger<ProductService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    
    public async Task<IEnumerable<Product>> GetProducts()
    {
        var products = await _unitOfWork.ProductRepository.GetAll();
        return products;
    }

    public async Task<bool> CreateProduct(string name)
    {
        var product = new Product { Name = name };
        await _unitOfWork.ProductRepository.Add(product);
        _unitOfWork.Save();
        return true;
    }
    
    public async Task<bool> SetSelectedProductsToShop(IEnumerable<string> selectedProductIds, string shopId)
    {
        List<Guid> guidList = selectedProductIds.Select(Guid.Parse).ToList();
        var products = await _unitOfWork.ProductRepository.GetByListId(guidList, "ProductId");
        var shop = await  _unitOfWork.ShopRepository.GetById(Guid.Parse(shopId));
        
        await _unitOfWork.Entry(shop).Collection(x => x.ShopProducts).LoadAsync();
        foreach (var product in products)
        {
            if (shop.ShopProducts.Any(x => x.ProductId == product.ProductId))
            {
                continue;
            }
            var shopProduct = new ShopProduct();
            shopProduct.ProductId = product.ProductId;
            shopProduct.Product = product;
            shopProduct.ShopId = shop.ShopId;
            shopProduct.Shop = shop;
            shop.ShopProducts.Add(shopProduct);
        }
        _unitOfWork.Save();
        return true;
    }

    public async Task<bool> EditProduct(Guid id, string name)
    {
        var product = await _unitOfWork.ProductRepository.GetById(id);
        product.Name = name;
        _unitOfWork.ProductRepository.Update(product);
        _unitOfWork.Save();
        return true;
    }

    public async Task<bool> DeleteProduct(Guid id)
    {
        var product = await _unitOfWork.ProductRepository.GetById(id);
        if (product == null) return false;
        _unitOfWork.ProductRepository.Delete(product);
        _unitOfWork.Save();
        return true;
    }

    public async Task<bool> DeleteProductFromShop(Guid shopId, Guid productId)
    {
        var shop = await  _unitOfWork.ShopRepository.GetById(shopId);
        await _unitOfWork.Entry(shop).Collection(x => x.ShopProducts).LoadAsync();
        var shopProduct = shop.ShopProducts.FirstOrDefault(x => x.ProductId == productId);
        shop.ShopProducts.Remove(shopProduct);
        _unitOfWork.Save();
        return true;
    }

    public async Task<Product> GetProductById(string id)
    {
        return await _unitOfWork.ProductRepository.GetById(Guid.Parse(id));
    }
}