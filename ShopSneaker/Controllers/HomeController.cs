using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopSneaker.Data;
using ShopSneaker.Models;
using System.Diagnostics;
using AutoMapper;
using ShopSneaker.Areas.Identity.Data;
using ShopSneaker.Data.Entities;

namespace ShopSneaker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ShopSneakerContext _context;
        private readonly IMapper _mapper;
        public HomeController(ILogger<HomeController> logger, ShopSneakerContext shopSneakerContext, IMapper mapper)
        {
            _logger = logger;
            _context = shopSneakerContext;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();
            List<ProductVm> productVms = _mapper.Map<List<ProductVm>>(products);
            return View(productVms);
        }
        public async Task<IActionResult> Detail(int id)
        {
            //var query = from p in _context.Products
            //            join c in _context.Categories on p.CategoryId equals c.Id into pc
            //            from c in pc.DefaultIfEmpty()
            //            where p.Id == id
            //            select new { p, pc, c };
            //query = query.OrderByDescending(c => c.p.Id);
            //var data = await query.Select(x => new ProductVm()
            //{
            //    Id = x.p.Id,
            //    Name = x.p.Name,
            //    Price = x.p.Price,
            //    Description = x.p.Description,
            //    CreateDate = x.p.CreateDate,
            //    Rating = x.p.Rating,
            //    Category = x.c.Name,
            //    ThumbPath = x.p.ThumbPath
            //}).ToListAsync();
            //return View(data);
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            return View(product);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}