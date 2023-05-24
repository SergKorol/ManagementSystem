using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShopManagementSystem.Application.ProductHandlers.Commands;
using ShopManagementSystem.Dashboard.Models.ViewModels;
using ShopManagementSystem.Services.UserServices;

namespace ShopManagementSystem.Dashboard.Controllers;

[AuthorizeAdministrator("Admin")]
public class ProductController : Controller
{
    private readonly IMediator _mediator;
    
    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public async Task<IActionResult> Index()
    {
        var products = await _mediator.Send(new AddGetProductsCommand());
        IEnumerable<ProductViewModel.ProductView> views = null;
        if (Constrains.ProductIds != null && Constrains.ProductIds.Any())
        {
            views = products.Where(x => !Constrains.ProductIds.Contains(x.ProductId.ToString())).Adapt<IEnumerable<ProductViewModel.ProductView>>();
            return View(views);
        }
        views = products.Adapt<IEnumerable<ProductViewModel.ProductView>>();
        return View(views);
    }

    public async Task<IActionResult> SetSelectedProductsToShop(IFormCollection form)
    {
        
        var productIds = form["selectedProducts"].ToList();
        if (string.IsNullOrEmpty(Constrains.ShopId) || !productIds.Any())
        {
            return RedirectToAction("Index");
        }
        var result = await _mediator.Send(new AddSetProductsToShopCommand { Ids = productIds, ShopId = Constrains.ShopId});
        if (!result) return RedirectToAction("Error", "Home");
        return RedirectToAction("GetShopById", "Shop", new { id = Constrains.ShopId });
    }

    public IActionResult GetShopId(string id, IEnumerable<string> productIds)
    {
        Constrains.ShopId = id;
        Constrains.ProductIds = productIds;
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> CreateProduct(string name)
    {
        var result = await _mediator.Send(new AddCreateProductCommand { Name = name});
        return !result ? RedirectToAction("Error", "Home") : RedirectToAction("Index");
    }
    
    public IActionResult CreateProductView()
    {
        return View("CreateProduct");
    }

    public async Task<IActionResult> EditProduct(Guid productId, string name)
    {
        var command = new AddEditProductCommand { Id = productId, Name = name};
        var result = await _mediator.Send(command);
        return !result ? RedirectToAction("Error", "Home") : RedirectToAction("Index");
    }
    
    public async Task<IActionResult> EditProductView(string id)
    {
        var command = new AddGetProductByIdCommand { Id = id };
        var shop = await _mediator.Send(command);
        var view = shop.Adapt<ProductViewModel.ProductDetail>();
        if (shop is null)
        {
            return RedirectToAction("Error", "Home");
        }
        return  View("EditProduct", view);
    }

    public async Task<IActionResult> GetProductById(string id)
    {
        var product = await _mediator.Send(new AddGetProductByIdCommand { Id = id});
        var view = product.Adapt<ProductViewModel.ProductDetail>();
        return View("ProductDetail", view);
    }

    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        var result = await _mediator.Send(new AddDeleteProductCommand { Id = id});
        if (!result) return RedirectToAction("Error", "Home");
        return RedirectToAction("Index");
    }
}

// public static class Relation
// {
//         public static string ShopId { get; set; }
//         public static IEnumerable<string> ProductIds { get; set; }
// }