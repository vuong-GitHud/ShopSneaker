using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopSneaker.Areas.Identity.Data;
using ShopSneaker.Data.Entities;
using ShopSneaker.Infacture.Services;
using ShopSneaker.Models;

namespace ShopSneaker.Controllers
{
    public class CartController : Controller
    {
        private readonly ShopSneakerContext _context;
        private readonly ICartService _cartService;

        public CartController(ShopSneakerContext context, ICartService cartService)
        {
            _context = context;
            _cartService = cartService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index(string userId)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var result = await _cartService.GetCart(userId);
            return View(result);
        }

        [HttpGet]
        public async Task<JsonResult> GetTotalCart(string userId)
        {
            var carts = await _context.Carts.Where(x => x.UserId.ToString() == userId).ToListAsync();

            var totalQuantity = carts.Sum(c => c.Quantity);

            return Json(totalQuantity);
        }
        
        [Authorize]
        [HttpPost]
        public async Task<JsonResult> AddToCart(int productId, string customerId, int quantity)
        {
            var addToCartResult = new AddToCartResult() { Success = false };

            if (quantity == 0)
            {
                addToCartResult.ErrorMessage = "The quantity must be larger than zero";
                addToCartResult.ErrorCode = "wrong-quantity";
                return Json(new {});
            }
            
            var cart = await _context.Carts.Where(x => x.UserId == new Guid(customerId)).FirstOrDefaultAsync();

            if (cart == null)
            {
                cart = new Cart()
                {
                    UserId = new Guid(customerId),
                    ProductId = productId,
                    Quantity = quantity,
                    DateCreated = DateTime.Now
                };

                await _context.AddAsync(cart);
            }
            else
            {
                var cartProduct = await _context.Carts
                    .Where(p => p.ProductId == productId && p.UserId.ToString() == customerId)
                    .FirstOrDefaultAsync();

                if (cartProduct == null)
                {
                    cart = new Cart()
                    {
                        UserId = new Guid(customerId),
                        ProductId = productId,
                        Quantity = quantity,
                        DateCreated = DateTime.Now
                    };

                    await _context.AddAsync(cart);
                }

                if (cartProduct != null)
                {
                    cartProduct.Quantity = cartProduct.Quantity + quantity;
                }

            }
            
            await _context.SaveChangesAsync();

            addToCartResult.Success = true;
            addToCartResult.TotalItems =
                await _context.Carts.Where(x => x.UserId.ToString() == customerId).CountAsync();
            
            return Json(addToCartResult);
        }
        //public async Task<IActionResult> Delete (String UserId)
        //{

        //}
    }
}