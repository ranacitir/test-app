using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using test_app.Context;
using test_app.Models.Product;

namespace test_app.Controllers
{
    public class ProductController : Controller
    {
        private readonly DataContext _dataContext;

        public ProductController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // GET: ProductController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_dataContext.Categories.ToList(), "Id", "Name");
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductAddModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new Product
                {
                    Name = model.Name,
                    Description = model.Description,
                    Mg = model.Mg,
                    Dose = model.Dose,
                    TotalMg = model.TotalMg,
                    Image = model.Image,
                    CategoryId = model.CategoryId,
                    SideEffects = model.SideEffects,
                    Status = model.Status,
                };

                _dataContext.Products.Add(entity);
                _dataContext.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.Categories = new SelectList(_dataContext.Categories.ToList(), "Id", "Name");
            return View(model);

        }

        // GET: ProductController/Edit/5
        public IActionResult Edit(int id)
        {
            var entity = _dataContext.Products.Select(p => new ProductEditModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Mg = p.Mg,
                Dose = p.Dose,
                TotalMg = p.TotalMg,
                Image = p.Image,
                CategoryId = p.CategoryId,
                SideEffects = p.SideEffects,
                Status = p.Status,
            }).FirstOrDefault(p => p.Id == id);

            ViewBag.Categories = new SelectList(_dataContext.Categories.ToList(), "Id", "Name");

            _dataContext.SaveChanges();
            return View(entity);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductEditModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            var entity = _dataContext.Products.FirstOrDefault(p => p.Id == model.Id);

            if(entity != null)
            {
                entity.Name = model.Name;
                entity.Description = model.Description;
                entity.Mg = model.Mg;
                entity.Dose = model.Dose;
                entity.TotalMg = model.TotalMg;
                entity.SideEffects = model.SideEffects;
                entity.CategoryId = model.CategoryId;
                entity.Status = model.Status;

                _dataContext.SaveChanges();
                
                return RedirectToAction("Index", "Home");
            }
                        
            return View(model);
           
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
