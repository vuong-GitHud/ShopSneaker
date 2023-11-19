using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopSneaker.Areas.Identity.Data;
using ShopSneaker.Data.Entities;
using ShopSneaker.Models;

namespace ShopSneaker.Infacture.Services;

public class OrderService : IOrderService
{
    private readonly IMapper _mapper;
    private readonly ShopSneakerContext _context;

    public OrderService(ShopSneakerContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<OrderDetailViewModel> OrderDetail(int id)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
        
        if (order == null)
        {
            return new OrderDetailViewModel();
        }
        
        var query = from o in _context.Orders
            join od in _context.OrderDetails on o.Id equals od.OrderId
            where od.OrderId == id
            select new { od };

        var orderDetail = await query.ToListAsync();

        OrderDetailViewModel model = new OrderDetailViewModel();
        model.UserId = order.UserId.ToString();
        model.Address = order.Address;
        model.Amount = order.Amount;
        model.City = order.City;
        model.Ward = order.Ward;
        model.District = order.District;
        model.PhoneNumber = order.PhoneNumber;
        model.Email = order.Email;
        model.Fullname = order.FullName;
        model.Id = order.Id;

        foreach (var item in orderDetail)
        {
            var productInfo = await _context.Products.FirstOrDefaultAsync(p => p.Id == item.od.ProductId);
            var product = new ProductVm()
            {
                Id = productInfo!.Id,
                Name = productInfo.Name,
                Price = productInfo.Price,
                CategoryId = productInfo.CategoryId
            };
            model.Quantity = item.od.Quantity;
            model.Products.Add(product);
        }

        return model;
    }

    public async Task<bool> CancleOrder(int id)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);

        if (order == null)
        {
            return false;
        }

        order.isCancle = true;
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<Order>> GetListOrder(string userId)
    {
        var order = await _context.Orders.Where(o => o.UserId.ToString() == userId).ToListAsync();
        return order;
    }
}