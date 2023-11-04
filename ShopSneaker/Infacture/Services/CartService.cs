using Microsoft.EntityFrameworkCore;
using ShopSneaker.Areas.Identity.Data;
using ShopSneaker.Models;

namespace ShopSneaker.Infacture.Services;

public class CartService : ICartService
{
    private readonly ShopSneakerContext _context;

    public CartService(ShopSneakerContext context)
    {
        _context = context;
    }

    public async Task<List<CartViewModel>> GetCart(string userId)
    {
        var carts = await _context.Carts.Where(c => c.UserId.ToString() == userId).ToListAsync();
        
        if (carts.Count() == 0)
        {
            return new List<CartViewModel>();
        }
        
        var cartList = new List<CartViewModel>();
        foreach (var item in carts)
        {
            var product = await _context.Products.Where(p => p.Id == item.ProductId).FirstOrDefaultAsync();

            var cart = new CartViewModel
            {
                ProductName = product!.Name,
                ProductImg = product.ThumbPath,
                Price = product.Price,
                Quantity = item.Quantity,
                TotalPrice = product.Price * item.Quantity
            };
            cartList.Add(cart);
        }

        return cartList;
    }
}