using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopSneaker.AdminMVC.Infrastructure;
using ShopSneaker.AdminMVC.Model;
using System.Security.Claims;
using ShopSneaker.AdminMVC.Helper;

namespace ShopSneaker.AdminMVC.Controllers
{
    public class AuthenController : BaseController
    {
        private readonly IAuthenAPI _authenApi;

        public AuthenController(IAuthenAPI authenApi)
        {
            _authenApi = authenApi;
        }

        [HttpGet]
        public IActionResult Login()
        {
            var model = new AuthenModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(AuthenModel request)
        {
            if (ModelState.IsValid)
            {
                var results = await _authenApi.Authenticate(request);
                if (results.UserRole.ToLower() != "admin")
                {
                    return View("Login", request);
                }
                if (results.AccessToken != null)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, results.Email),
                        new Claim(ClaimTypes.Rsa, results.AccessToken),
                        new Claim(ClaimTypes.NameIdentifier, results.UserId),
                        new Claim(ClaimTypes.Role, results.UserRole)
                    };

                    var claimsIdentity = new ClaimsIdentity(
                            claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = request.RememberMe,
                        ExpiresUtc = WebHelper.ConvertUnixTimeToDate(results.Expired)
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    //if (!string.IsNullOrWhiteSpace(returnUrl))
                    //    return Redirect(returnUrl);
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    ViewBag.Error = results.Message;
                    return View("Login", request);
                }
            }
            return View("Login", request);
        }
    }
}
