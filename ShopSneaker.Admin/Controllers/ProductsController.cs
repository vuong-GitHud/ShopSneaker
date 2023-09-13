using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopSneaker.Areas.Identity.Data;
using ShopSneaker.Data.Entities;
using ShopSneaker.Models;

namespace ShopSneaker.Admin.Controllers;

public class ProductsController : Controller
{
    private readonly ShopSneakerContext _context;
    private readonly IMapper _mapper;
    public ProductsController(ShopSneakerContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    // GET
    public async Task<IActionResult> Index()
    {
        var query = from p in _context.Products
            join c in _context.Categories on p.CategoryId equals c.Id into pc
            from c in pc.DefaultIfEmpty()
            select new { p, pc, c };
        query = query.OrderByDescending(c => c.p.Id);
        var data = await query.Select(x => new ProductVm()
        {
            Id = x.p.Id,
            Name = x.p.Name,
            Price = x.p.Price,
            Description = x.p.Description,
            CreateDate = x.p.CreateDate,
            Rating = x.p.Rating,
            Category = x.c.Name,
            ThumbPath = x.p.ThumbPath
        }).ToListAsync();
        return View(data);
    }
}