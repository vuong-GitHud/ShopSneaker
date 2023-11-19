using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopSneaker.Areas.Identity.Data;
using ShopSneaker.Data.Entities;

namespace ShopSneaker.AdminMVC.Controllers
{
    public class OrderAdminController : Controller
    {
        private readonly ShopSneakerContext _context;

        public OrderAdminController(ShopSneakerContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var order = await _context.Orders.OrderByDescending(o => o.OrderDate).ToListAsync();

            if (order.Count == 0)
            {
                return View(new List<Order>());
            }
            
            return View(order);
        }
    }
}