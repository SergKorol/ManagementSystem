using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ShopManagementSystem.Dashboard.Controllers;

public class ProductController : Controller
{
    
    private readonly IMediator _mediator;
    
    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult CreateProduct()
    {
        _mediator.Send(null);
        return null;
    }

    public IActionResult UpdateProduct()
    {
        _mediator.Send(null);
        return null;
    }

    public IActionResult GetProductById()
    {
        _mediator.Send(null);
        return null;
    }

    public IActionResult GetAllProducts()
    {
        _mediator.Send(null);
        return null;
    }

    public IActionResult DeleteProduct()
    {
        _mediator.Send(null);
        return null;
    }

    public IActionResult GetProductsByShop()
    {
        _mediator.Send(null);
        return null;
    }
    
    
    
}