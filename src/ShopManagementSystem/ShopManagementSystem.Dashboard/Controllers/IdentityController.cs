using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ShopManagementSystem.Dashboard.Controllers;

public class IdentityController : Controller
{

    private readonly IMediator _mediator;
    
    public IdentityController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult GetAllUsers()
    {
        _mediator.Send(null);
        return null;
    }


    public IActionResult GetUserById()
    {
        _mediator.Send(null);
        return null;
    }

    public IActionResult CreateUser()
    {
        _mediator.Send(null);
        return null;
    }


    public IActionResult DeleteUser()
    {
        _mediator.Send(null);
        return null;
    }
}