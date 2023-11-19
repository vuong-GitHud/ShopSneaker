using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopSneaker.Infacture.Services;

namespace ShopSneaker.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrdersController(IOrderService orderService, IHttpContextAccessor httpContextAccessor)
        {
            _orderService = orderService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier);
            var result = await _orderService.GetListOrder(userId!.Value.ToString());
            return View(result);
        }

        public async Task<IActionResult> OrderDetail(int id)
        {
            var result = await _orderService.OrderDetail(id);
            return View(result);
        }
        
        public async Task<IActionResult> CancleOrder(int id)
        {
            var result = await _orderService.CancleOrder(id);

            if (result)
            {
                return RedirectToAction("Index", "Orders");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}