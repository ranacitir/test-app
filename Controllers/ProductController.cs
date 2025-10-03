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
        public async Task<IActionResult> Create(ProductAddModel model)
        {
            if (ModelState.IsValid)
            {
                var fileName = Path.GetRandomFileName() + ".jpg";
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await model.Image!.CopyToAsync(stream);
                }

                var entity = new Product
                {
                    Name = model.Name,
                    Description = model.Description,
                    Mg = model.Mg,
                    Dose = model.Dose,
                    TotalMg = model.TotalMg,
                    Image = fileName,
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
                ImageName = p.Image,
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
        public async Task<IActionResult> Edit(int id, ProductEditModel model, IFormFile file)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            var entity = _dataContext.Products.FirstOrDefault(p => p.Id == model.Id);

            if(entity != null)
            {
                if (file.Length > 0)
                {
                    var fileName = file.FileName + ".jpg";
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fileName);
                    
                    await using FileStream stream = new(path, FileMode.Create);
                    await file.CopyToAsync(stream);

                    entity.Image = fileName;
                }

                entity.Name = model.Name;
                entity.Description = model.Description;
                entity.Mg = model.Mg;
                entity.Dose = model.Dose;
                entity.TotalMg = model.TotalMg;
                entity.SideEffects = model.SideEffects;
                entity.CategoryId = model.CategoryId;
                entity.Status = model.Status;

                _dataContext.SaveChanges();

                TempData["Message"] = $"{entity.Name} adlı ürün güncellendi";

                return RedirectToAction("Index", "Home");
            }
                        
            return View(model);
           
        }

        // GET: ProductController/Delete/5
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var entity = _dataContext.Products.FirstOrDefault(i => i.Id == id);

            if (entity != null)
            {
                return View(entity);
            }

            return RedirectToAction("Index");
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int? id, IFormCollection collection)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var entity = _dataContext.Products.FirstOrDefault(i => i.Id == id);

            if(entity != null)
            {
                _dataContext.Products.Remove(entity);
                _dataContext.SaveChanges();
                TempData["Message"] = $"{entity.Name} ürünü silindi";
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
