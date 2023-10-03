using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopSneaker.AdminMVC.Model.Product;
using ShopSneaker.Areas.Identity.Data;
using ShopSneaker.Data.Entities;
using ShopSneaker.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace ShopSneaker.AdminMVC.Controllers
{
    public class ProductAdminController : Controller
    {
        // GET: ProductController
        private readonly ShopSneakerContext _context;
        private readonly IMapper _mapper;
        private IHostingEnvironment _environment;


        public ProductAdminController(ShopSneakerContext context, IMapper mapper, IHostingEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _environment = environment;

        }
        // GET: ProductController
        public async Task<IActionResult> Index()
        {
            var query = from p in _context.Products
                        join c in _context.Categories on p.CategoryId equals c.Id into pc
                        from c in pc.DefaultIfEmpty()
                        select new { p, pc, c };
            query = query.OrderByDescending(c => c.p.Id);
            var data = await query.Select(x => new Model.Product.ProductVm()
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


        // GET: ProductController/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create(Model.Product.ProductVm model)
        {
            if (model.Files != null)
            {
                var file = Path.Combine(_environment.ContentRootPath, "uploads", model.Files.FileName);
                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await model.Files.CopyToAsync(fileStream);
                }
                var fileName = Path.GetFileName(model.Files.FileName);
                model.ThumbPath = "uploads/" + fileName;
            }
            model.CreateDate = DateTime.Now;
            model.Rating = 5;
            _context.Products.Add(_mapper.Map<Product>(model));
            await _context.SaveChangesAsync();
            return RedirectToAction("Create");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (product != null)
            {
                var result = _mapper.Map<UpdateProductVm>(product);
                return View(result);
            }
            return RedirectToAction("Index");
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateProductVm model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var product = await _context.Products.FindAsync(model.Id);
            if (product == null)
            {
                return View(model);
            }
            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.CategoryId = model.CategoryId;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: ProductController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            var productImg = await _context.ProductImgs.Where(x => x.ProductId == id).FirstOrDefaultAsync();
            if (productImg != null)
            {
                _context.ProductImgs.Remove(productImg);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
