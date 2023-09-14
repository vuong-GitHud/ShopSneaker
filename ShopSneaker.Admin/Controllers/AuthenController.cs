using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using ShopSneaker.Data.Entities;
using ShopSneaker.Models;
using System.Security.Claims;

namespace ShopSneaker.Admin.Controllers
{
    public class AuthenController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        public AuthenController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AuthenVm model)
        {
            if (ModelState.IsValid)
            {

                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return RedirectToAction("Authen");
                }
                var checkPassword = await _userManager.CheckPasswordAsync(user, model.Password);
                if (checkPassword)
                {
                    var role = _userManager.GetRolesAsync(user).Result.FirstOrDefault();
                    var result = new UserViewModel()
                    {
                        Email = user.Email,
                        DOB = user.DOB,
                        FullName = user.FullName,
                        Role = role,
                        UserName = user.UserName,
                        Id = user.Id.ToString(),
                    };
                    if (result.Role.ToLower() != "admin")
                    {
                        return View();
                    }
                    var claim = new List<Claim>();
                    {
                        new Claim(ClaimTypes.Name, result.Email);
                        new Claim(ClaimTypes.Role, result.Role);
                        new Claim(ClaimTypes.NameIdentifier, result.Id);

                    }
                    var claimsIdentity = new ClaimsIdentity(
                            claim, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity));
                    return RedirectToAction("Index", "Product");
                }
                return View();
            }
            return View();
        }

        // GET: AuthenController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuthenController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthenController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AuthenController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthenController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AuthenController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
