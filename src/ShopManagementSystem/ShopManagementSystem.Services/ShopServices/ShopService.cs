using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopManagementSystem.Application.Dependencies;
using ShopManagementSystem.Data;
using ShopManagementSystem.Data.Models;

namespace ShopManagementSystem.Services.ShopServices;

public sealed class ShopService : IShopService
{
    private readonly IUnitOfWork _unitOfWork;
    private ILogger<ShopService> _logger;

    public ShopService(IUnitOfWork unitOfWork, ILogger<ShopService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<IEnumerable<Shop>> GetShops()
    {
        var shops = await _unitOfWork.ShopRepository.GetAll();
        foreach (var shop in shops)
        {
            await _unitOfWork.Entry(shop).Collection(x => x.ShopProducts)
                .Query().Include(sp => sp.Product).LoadAsync();
        }

        return shops;
    }

    public async Task<bool> CreateShop(string title, string phone)
    {
        try
        {
            var shop = new Shop { Title = title, Phone = phone };
            await _unitOfWork.ShopRepository.Add(shop);
            _unitOfWork.Save();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return false;
        }
    }

    public async Task<Shop> GetShopById(string id)
    {
        var shop = await _unitOfWork.ShopRepository.GetById(Guid.Parse(id));
        await _unitOfWork.Entry(shop).Collection(x => x.ShopProducts)
            .Query().Include(sp => sp.Product).LoadAsync();
        await _unitOfWork.Entry(shop).Collection(x => x.Employees).LoadAsync();
        
        return shop;
    }
    
    public async Task<bool> EditShop(Guid id, string title, string phone)
    {
        try
        {
            var shop = await _unitOfWork.ShopRepository.GetById(id);
            shop.Title = title;
            shop.Phone = phone;
            _unitOfWork.ShopRepository.Update(shop);
            _unitOfWork.Save();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return  false;
        }
    }

    public async Task<bool> DeleteShop(Guid id)
    {
        try
        {
            var shop = await _unitOfWork.ShopRepository.GetById(id);
            if (shop == null) return false;
            _unitOfWork.ShopRepository.Delete(shop);
            _unitOfWork.Save();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return false;
        }
    }

    public async Task UpdateShop(Shop shop)
    {
        try
        {
            _unitOfWork.ShopRepository.Update(shop);
            _unitOfWork.Save();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
    }
}