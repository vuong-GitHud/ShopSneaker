using Microsoft.AspNetCore.Mvc;

namespace ShopSneaker.Admin.Controllers;

public class BaseController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}