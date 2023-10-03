using Microsoft.AspNetCore.Mvc;

namespace ShopSneaker.Admin.Controllers;

public class LoginController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}