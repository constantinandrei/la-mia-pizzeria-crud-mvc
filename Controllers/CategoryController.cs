using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_static.Controllers
{
    public class CategoryController : Controller
    {
        private PizzaDbContext db;

        public CategoryController()
        {
            db = new PizzaDbContext();
        }
        public IActionResult Index()
        {
            ViewData["Title"] = "Categorie";
            List<Category> categories = db.Categories.ToList();
            return View(categories);
        }

        public IActionResult Detail(int id)
        {
            ViewData["Title"] = "Categorie";
            Category category = db.Categories.Where(c => c.Id == id).FirstOrDefault();
            if (category == null)
                return NotFound();
            return View(category);
        }
        public IActionResult Create()
        {
            ViewData["Title"] = "Crea Categoria";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            ViewData["Title"] = "Crea Categoria";
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            db.Categories.Add(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            Category category = db.Categories.Where(c => c.Id == id).FirstOrDefault();
            if (category == null)
                return NotFound();
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Category category)
        {
            category.Id = id;
            if (!ModelState.IsValid)
                return View(category);
            db.Categories.Update(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Category category = db.Categories.Where(c => c.Id == id).FirstOrDefault();
            if (category == null)
                return NotFound();
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
