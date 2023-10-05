using Microsoft.AspNetCore.Mvc;
using ShopSneaker.Admin.Infrastructure;
using ShopSneaker.Admin.Models;

namespace ShopSneaker.Admin.Controllers;

public class LoginController : Controller
{
    private readonly IUserManagerService _userManagerService;
    private readonly IUserService _userService;

    public LoginController(IUserManagerService userManagerService, IUserService userService)
    {
        _userManagerService = userManagerService;
        _userService = userService;
    }
    // GET
    [HttpGet]
    public IActionResult Index()
    {
        var model = new LoginVm();
        return View(model);
    }

    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Index(LoginVm model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var loginResult = await _userManagerService.LoginAsync(model.Email, model.Password);
        if (!loginResult)
        {
            ModelState.AddModelError(string.Empty, "Username or Password is incorrect");
            return View(model);
        }

        var user = await _userService.GetUserByEmail(model.Email);

        await _userManagerService.SignInAsync(user!, model.RememberMe);

        return string.IsNullOrEmpty(model.RequestPath)
            ? RedirectToAction("Index", "Product")
            : Redirect(model.RequestPath);
    }
}