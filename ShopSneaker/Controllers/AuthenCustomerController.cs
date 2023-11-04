using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ShopSneaker.Infacture.Helper;
using ShopSneaker.Infacture.interfaces;
using ShopSneaker.Models;

namespace ShopSneaker.Controllers
{
    public class AuthenCustomerController : Controller
    {
        private readonly IAuthenApi _authenApi;

        public AuthenCustomerController(IAuthenApi authenApi)
        {
            _authenApi = authenApi;
        }

        [HttpGet]
        public IActionResult Login()
        {
            var model = new AuthenVm();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(AuthenVm request)
        {
                if (ModelState.IsValid)
            {
                var results = await _authenApi.Authenticate(request);
                if (results.AccessToken != null)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, results.Email),
                        new Claim(ClaimTypes.Email, results.Email),
                        new Claim(ClaimTypes.Surname, results.Name),
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
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = results.Message;
                    return View("Login", request);
                }
            }
            return View("Login", request);
        }
        
        [HttpGet]
        public IActionResult Register()
        {
            var model = new RegisterModel();
            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var result = await _authenApi.CheckEmailExist(model.Email);
            
            if (result)
            {
                ViewBag.Message = "Email already exists";
                return View("Register");
            }

            if (model.PasswordConfirm.Equals(model.Password))
            {
                ViewBag.Message = "Password and ConfirmPass are not same";
                return View("Register");
            }
            
            var registerResults = await _authenApi.Register(model);
            
            if (registerResults.IsSuccess == false)
            {
                ViewBag.Message = "Please input field again";
                return View("Register");
            }
            
            ViewBag.Message = "Register Successed!!!";
            return View("Login");
        }
        
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
