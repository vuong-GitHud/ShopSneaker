using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopSneaker.Areas.Identity.Data;
using ShopSneaker.Data.Entities;
using ShopSneaker.Models;

namespace ShopSneaker.AdminMVC.Controllers
{
    public class CategoryController : Controller
    {

        private readonly ShopSneakerContext _context;
        //private readonly IMapper _mapper;

        //public CategoryController (ShopSneakerContext context, IMapper mapper)
        //{
        //    _context = context;
        //    _mapper = mapper;

        //}
        public CategoryController(ShopSneakerContext context)
        {
            _context = context;


        }
        // GET: CategoryController
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.ToListAsync();
            //List<CategoryVm> categories1 = _mapper.Map<List<CategoryVm>>(categories);
            return View(categories);
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CategoryController/Create
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create(CategoryVm model)
        {
            var category = new Category()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
            };
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        // POST: CategoryController/Create
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

        [HttpGet]
        // GET: CategoryController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var category = await _context.Categories.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (category != null)
            {
                var viewModel = new CategoryVm()
                {
                    Name = category.Name,
                    Description = category.Description
                };
                return View(viewModel);
            }
            return RedirectToAction("Index");
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CategoryVm model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var category = await _context.Categories.FindAsync(model.Id);
            if (category == null)
            {
                return View(model);
            }
            category.Name = model.Name;
            category.Description = model.Description;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        // POST: CategoryController/Delete/5      
        public async Task<ActionResult> Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");
        }

    }
}
