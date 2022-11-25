using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models;
using la_mia_pizzeria_static.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Controllers
{
    public class CategoryController : Controller
    {
        private IDbCategoryRepository categoryRepository;

        public CategoryController()
        {
          categoryRepository = new ListCategoryRepository();
        }
        public IActionResult Index()
        {
            ViewData["Title"] = "Categorie";
            List<Category> categories = categoryRepository.Get();
            return View(categories);
        }

        public IActionResult Detail(int id)
        {
            ViewData["Title"] = "Categorie";
            Category category = categoryRepository.Get(id);
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
            categoryRepository.Create(category);
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            if (!categoryRepository.Exists(id))
                return NotFound();
            return View(categoryRepository.Get(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Category category)
        {
            category.Id = id;
            if (!ModelState.IsValid)
                return View(category);
            categoryRepository.Update(category);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            if (!categoryRepository.Exists(id))
                return NotFound();
            if (categoryRepository.HasConstraint(id))
            {
                return View("ImpossibleDelete");
            }
            categoryRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
