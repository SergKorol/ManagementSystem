using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShopManagementSystem.Application.ProductHandlers.Commands;
using ShopManagementSystem.Application.ShopHandlers.Commands;
using ShopManagementSystem.Application.UserHandlers.Commands;
using ShopManagementSystem.Dashboard.Models.ViewModels;
using ShopManagementSystem.Services.UserServices;

namespace ShopManagementSystem.Dashboard.Controllers;

[AuthorizeAdministrator("Admin")]
public class ShopController : Controller
{
    
    private readonly IMediator _mediator;
    
    public ShopController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public async Task<IActionResult> Index()
    {
        Constrains.ShopId = null;
        var command = new AddGetShopsCommand();
        var result = await _mediator.Send(command);
        var views = result.Adapt<IEnumerable<ShopViewModel.ShopView>>();
        return View(views);
    }

    public async Task<IActionResult> CreateShop(string title, string phone)
    {
        var command = new AddCreateShopCommand { Title = title, Phone = phone};
        var result = await _mediator.Send(command);
        return !result ? RedirectToAction("Error", "Home") : RedirectToAction("Index");
    }

    public IActionResult CreateShopView()
    {
            return View("CreateShop");
    }

    public async Task<IActionResult> GetShopById(string id)
    {
        var command = new AddGetShopByIdCommand { Id = id };
        var shop = await _mediator.Send(command);
        var view = shop.Adapt<ShopViewModel.ShopDetail>();
        if (shop is null)
        {
            return RedirectToAction("Error", "Home");
        }

        Constrains.ShopId = null;
        return  View("ShopDetail", view);
    }
    

    public async Task<IActionResult> EditShop(Guid shopId, string title, string phone)
    {
        var command = new AddEditShopCommand { Id = shopId, Title = title, Phone = phone};
        var result = await _mediator.Send(command);
        return !result ? RedirectToAction("Error", "Home") : RedirectToAction("Index");
    }
    
    public async Task<IActionResult> EditShopView(string id)
    {
        var command = new AddGetShopByIdCommand { Id = id };
        var shop = await _mediator.Send(command);
        var view = shop.Adapt<ShopViewModel.ShopDetail>();
        if (shop is null)
        {
            return RedirectToAction("Error", "Home");
        }
        return  View("EditShop", view);
    }

    public async Task<IActionResult> DeleteShop(Guid id)
    {
        var command = new AddDeleteShopCommand { Id = id };
        var result = await _mediator.Send(command);
        if (!result) return RedirectToAction("Error", "Home");
        return RedirectToAction("Index");
    }

    public IActionResult SetProduct(string id, IEnumerable<string> productIds)
    {
        return RedirectToAction("GetShopId", "Product", new { id, productIds });
    }
    
    public IActionResult SetEmployee(string id, IEnumerable<string> employeeIds)
    {
        return RedirectToAction("GetShopId", "User", new { id, employeeIds });
    }
    
    
    public IActionResult DeleteProduct(string id)
    {
        return RedirectToAction("GetShopId", "Product", new { id });
    }

    public async Task<IActionResult> DeleteProductFromShop(Guid shopId, Guid productId)
    {
        var command = new AddDeleteProductFromShop { ShopId = shopId, ProductId = productId };
        var result = await _mediator.Send(command);
        return !result ? RedirectToAction("Error", "Home") : RedirectToAction("Index");
    }


    public async Task<IActionResult> DeleteEmployeeFromShop(Guid shopId, Guid employeeId)
    {
        var command = new AddDeleteEmployeeFromShopCommand { ShopId = shopId, EmployeeId = employeeId };
        var result = await _mediator.Send(command);
        return !result ? RedirectToAction("Error", "Home") : RedirectToAction("Index");
    }
}