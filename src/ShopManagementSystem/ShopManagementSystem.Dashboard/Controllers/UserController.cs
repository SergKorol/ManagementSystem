using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShopManagementSystem.Application.UserHandlers.Commands;
using ShopManagementSystem.Dashboard.Models.Inputs;
using ShopManagementSystem.Dashboard.Models.ViewModels;
using ShopManagementSystem.Services.UserServices;

namespace ShopManagementSystem.Dashboard.Controllers;

public class UserController : Controller
{

    private readonly IMediator _mediator;
    private readonly ILogger<UserController> _logger;
    
    public UserController(IMediator mediator, ILogger<UserController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    
    [AuthorizeAdministrator("Admin")]
    public async Task<IActionResult> Index()
    {
        var command = new AddGetUsersCommand();
        var users = await _mediator.Send(command);
        List<UserViewModel.UserView> views = null;
        if (Constrains.ProductIds != null && Constrains.ProductIds.Any())
        {
            users.Where(x => !Constrains.EmployeeIds.Contains(x.Id.ToString()))
                .Adapt<IEnumerable<UserViewModel.UserView>>();
            return View(views);
        }
        views = users.Adapt<List<UserViewModel.UserView>>();
        return View(views);
    }
    
    public async Task<IActionResult> Login(UserAuthInput userAuthInput)
    {
        var command = new AddUserLoginCredentials { Login = userAuthInput.Email, Password = userAuthInput.Password };
        var token = await _mediator.Send(command);
        if (string.IsNullOrEmpty(token)) return Redirect("LoginView");
        Response.Cookies.Append("Token", token);
        return RedirectToAction("Index", "Home");
    }

    public IActionResult LoginView()
    {
        return  View("Login");
    }
    [AuthorizeAdministrator("Admin")]
    public async Task<IActionResult> Register(UserAuthInput userAuthInput)
    {
        if (userAuthInput.Password != userAuthInput.ConfirmPassword)
        {
            _logger.LogError("Passwords are not the same");   
            return  Redirect("FailRegister");
        }
        var command = new AddRegisterNewUserCommand { Login = userAuthInput.Email, Password = userAuthInput.Password };
        var result = await _mediator.Send(command);
        if (!result) return Redirect("RegisterView");
        return RedirectToAction("Index", "Home");
    }
    [AuthorizeAdministrator("Admin")]
    public IActionResult RegisterView()
    {
        return  View("Register");
    }
    [AuthorizeAdministrator("Admin")]
    public async Task<IActionResult> GetUserById(string id)
    {
        var user = await _mediator.Send(new AddGetUserByIdCommand { Id = id });
        var view = user.Adapt<UserViewModel.UserDetailView>();
        return View("UserDetail", view);
    }
    [AuthorizeAdministrator("Admin")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var result = await _mediator.Send(new AddDeleteUserCommand {Id = id});
        if (!result) return RedirectToAction("Error", "Home");
        return RedirectToAction("Index");
    }
    [AuthorizeAdministrator("Admin")]
    public IActionResult GetShopId(string id, IEnumerable<string> employeeIds)
    {
        Constrains.ShopId = id;
        Constrains.ProductIds = employeeIds;
        return RedirectToAction("Index");
    }
    [AuthorizeAdministrator("Admin")]
    public async Task<IActionResult> SetSelectedUsersToEmployeeInShop(IFormCollection form)
    {
        var userIds = form["selectedUsers"].ToList();
        var firstName = form["firstName"].ToString();
        var lastName = form["lastName"].ToString();
        var post = form["post"].ToString();
        if (string.IsNullOrEmpty(Constrains.ShopId) || !userIds.Any() || string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(post))
        {
            return RedirectToAction("Index");
        }

        var command = new AddSetSelectedUsersToEmployeeInShopCommand
        {
            Ids = userIds, ShopId = Constrains.ShopId, FirstName = firstName, LastName = lastName, Post = post
        };
        var result = await _mediator.Send(command);
        if (!result) return RedirectToAction("Error", "Home");
        return RedirectToAction("GetShopById", "Shop", new { id = Constrains.ShopId });
    }
}