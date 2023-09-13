using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopSneaker.Data;
using ShopSneaker.Models;
using System.Diagnostics;
using AutoMapper;
using ShopSneaker.Areas.Identity.Data;

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
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}