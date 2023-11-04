using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopSneaker.Areas.Identity.Data;
using ShopSneaker.Data.Entities;
using ShopSneaker.Infacture.Helper;
using ShopSneaker.Infacture.interfaces;
using ShopSneaker.Infacture.Services;
using ShopSneaker.Models;
using VNPayPackage;

namespace ShopSneaker.Controllers;

public class CheckoutController : Controller
{
    private readonly ICartService _cartService;
    private readonly IAuthenApi _authenApi;
    private readonly ShopSneakerContext _context;
    private readonly IConfiguration _configuration;

    public CheckoutController(ICartService cartService, IAuthenApi authenApi, ShopSneakerContext context, IConfiguration configuration)
    {
        _cartService = cartService;
        _authenApi = authenApi;
        _context = context;
        _configuration = configuration;
    }

    // GET
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<OrderViewModel>> Index(string userId)
    {
        var cart = await _cartService.GetCart(userId);
        var user = await _authenApi.GetInfo(userId);
        OrderViewModel model = new OrderViewModel();
        model.Carts = cart;
        model.Email = user.Email;
        model.Fullname = user.FullName;
        model.UserId = user.UserId;
        model.Amount = cart.Sum(c => c.TotalPrice);
        return View(model);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateOrder(OrderViewModel model)
    {
        var order = new Order()
        {
            OrderDate = DateTime.Now,
            UserId = new Guid(model.UserId!),
            Email = model.Email,
            Address = model.Address,
            PhoneNumber = model.PhoneNumber,
            City = model.City,
            District = model.District,
            Ward = model.District,
            FullName = model.Fullname,
            CurrencyCode = string.Empty,
            isPayment = false,
            isCancle = false,
            PaymentId = string.Empty,
            PostCode = string.Empty,
            Amount = model.Amount
        };
        await _context.AddAsync(order);
        await _context.SaveChangesAsync();
        
        return RedirectToAction("Checkout",new {orderId = order.Id});
    }

    [HttpGet]
    public async Task<IActionResult> Checkout(int orderId)
    {
        var tmnCode = _configuration["VNpay:TmnCode"];
        var returnUrl = _configuration["VNpay:ReturnUrl"];
        var cancelUrl = _configuration["VNpay:CancelUrl"];
        var hashSerect = _configuration["VNpay:HashSecret"];

        var order = await _context.Orders.FirstOrDefaultAsync(c => c.Id == orderId);

        VnPayLibrary vnpay = new VnPayLibrary();
        vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
        vnpay.AddRequestData("vnp_Command", "pay");
        vnpay.AddRequestData("vnp_TmnCode", tmnCode);
        vnpay.AddRequestData("vnp_Amount", (Convert.ToInt32(order.Amount) * 100).ToString());
        vnpay.AddRequestData("vnp_CreateDate", order.OrderDate.ToString("yyyyMMddHHmmss"));
        vnpay.AddRequestData("vnp_CurrCode", "VND");
        vnpay.AddRequestData("vnp_BankCode", "VNBANK");
        vnpay.AddRequestData("vnp_IpAddr", "192.168.1.1");
        vnpay.AddRequestData("vnp_Locale", "vn");
        vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + order.Id);
        vnpay.AddRequestData("vnp_OrderType", "other");
        vnpay.AddRequestData("vnp_ReturnUrl", $"https://localhost:7170/Checkout/SuccessPayment/{orderId}");
        vnpay.AddRequestData("vnp_TxnRef", order.Id.ToString());

        string paymentUrl = vnpay.CreateRequestUrl("https://sandbox.vnpayment.vn/paymentv2/vpcpay.html", hashSerect);
        return Redirect(paymentUrl);
    }

    [HttpGet]
    public async Task<ActionResult> SuccessPayment([FromQuery] string vnp_TxnRef)
    {
        var orderId = Convert.ToInt32(vnp_TxnRef);
        var order = await _context.Orders.FirstOrDefaultAsync(c => c.Id == orderId);
        var carts = await _context.Carts.Where(c => c.UserId == order.UserId).ToListAsync();

        order.isPayment = true;
        await _context.SaveChangesAsync();

        foreach (var item in carts)
        {
            var orderDetail = new OrderDetail()
            {
                OrderId = order.Id,
                ProductId = item.ProductId,
                Price = item.Price,
                Quantity = item.Quantity
            };
            _context.OrderDetails.Add(orderDetail);
            await _context.SaveChangesAsync();
        }

        _context.Carts.RemoveRange(carts);
        await _context.SaveChangesAsync();
        
        return View();
    }
}