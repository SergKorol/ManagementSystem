using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ShopManagementSystem.Dashboard.Controllers;

public class ShopController : Controller
{
    
    private readonly IMediator _mediator;
    
    public ShopController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult CreateShop()
    {
        _mediator.Send(null);
        return null;
    }

    public IActionResult GetShopById()
    {
        _mediator.Send(null);
        return null;
    }

    public IActionResult UpdateShop()
    {
        _mediator.Send(null);
        return null;
    }

    public IActionResult DeleteShop()
    {
        _mediator.Send(null);
        return null;
    }

    public IActionResult SetEmployee()
    {
        _mediator.Send(null);
        return null;
    }


    public IActionResult SetProduct()
    {
        _mediator.Send(null);
        return null;
    }


    public IActionResult DeleteEmployeeFromShop()
    {
        _mediator.Send(null);
        return null;
    }

    public IActionResult DeleteProductFromShop()
    {
        _mediator.Send(null);
        return null;
    }

}