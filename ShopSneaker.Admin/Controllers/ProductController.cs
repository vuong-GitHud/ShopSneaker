using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopSneaker.Areas.Identity.Data;
using ShopSneaker.Models;

namespace ShopSneaker.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly ShopSneakerContext _context;
        private readonly IMapper _mapper;

        public ProductController(ShopSneakerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET: ProductController
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

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
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

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
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

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
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
