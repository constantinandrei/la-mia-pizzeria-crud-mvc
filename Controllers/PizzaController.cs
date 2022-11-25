using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models;
using la_mia_pizzeria_static.Models.Form;
using la_mia_pizzeria_static.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.SqlServer.Server;
using System.Data;

namespace la_mia_pizzeria_static.Controllers
{
    
    public class PizzaController : Controller
    {
        //private PizzaDbContext db;
        private DbPizzaRepository pizzaRepository;
        private DbCategoryRepository categoryRepository;
        private DbIngredientRepository ingredientRepository;

        public PizzaController()
        {
            //db = new PizzaDbContext();
            pizzaRepository = new DbPizzaRepository();
            categoryRepository = new DbCategoryRepository();
            ingredientRepository = new DbIngredientRepository();
        }
        public IActionResult Index()
        {
            ViewData["Title"] = "Todi Pizza";
            return View(pizzaRepository.Get());
        }
        // filtro per pizze con categoria
        public IActionResult CategoryFilter(int categoryId)
        {

            List<Pizza> Pizze = pizzaRepository.GetByCategoryId(categoryId);
            ViewData["Title"] = "Todi Pizza";
            return View("Index", Pizze);
        }

        public IActionResult Detail(int id)
        {
            Pizza pizza = pizzaRepository.Get(id);
            ViewData["Title"] = "Todi Pizza | " + pizza.Name;
            return View(pizza);
        }
        public IActionResult Create()
        {
            PizzaForm formData = new PizzaForm();
            formData.Pizza = new Pizza();
            formData.Categories = categoryRepository.Get();
            formData.Ingredients = new List<SelectListItem>();
            List<Ingredient> IngredientsList = ingredientRepository.Get();
            foreach(Ingredient ingredient in IngredientsList)
            {
                formData.Ingredients.Add(new SelectListItem(ingredient.Name, ingredient.Id.ToString()));
            }
            return View(formData);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaForm formData)
        {
            if (!ModelState.IsValid)
            {
                formData.Categories = categoryRepository.Get();
                formData.Ingredients = new List<SelectListItem>();
                List<Ingredient> IngredientsList = ingredientRepository.Get();
                foreach (Ingredient ingredient in IngredientsList)
                {
                    formData.Ingredients.Add(new SelectListItem(ingredient.Name, ingredient.Id.ToString()));
                }
                return View(formData);
            }

            pizzaRepository.Create(formData.Pizza, formData.SelectedIngredients);

            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            PizzaForm formData = new PizzaForm();
            formData.Pizza = pizzaRepository.Get(id);
            if (formData.Pizza == null)
                return NotFound();
            formData.Ingredients = new List<SelectListItem>();
            List<Ingredient> IngredientsList = ingredientRepository.Get();
            foreach (Ingredient ingredient in IngredientsList)
            {
                bool selected = formData.Pizza.Ingredients.Any(i => i.Id == ingredient.Id);
                formData.Ingredients.Add(new SelectListItem(ingredient.Name, ingredient.Id.ToString(), selected));
            }
            formData.Categories = categoryRepository.Get();
            return View(formData);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, PizzaForm formData)
        {
            formData.Pizza.Id = id;
            if (!ModelState.IsValid)
            {
                formData.Categories = categoryRepository.Get();
                formData.Ingredients = new List<SelectListItem>();
                List<Ingredient> IngredientsList = ingredientRepository.Get();
                foreach (Ingredient ingredient in IngredientsList)
                {
                    formData.Ingredients.Add(new SelectListItem(ingredient.Name, ingredient.Id.ToString()));
                }
                return View(formData);
            }

            pizzaRepository.Update(formData.Pizza, formData.SelectedIngredients);

            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Pizza pizza = pizzaRepository.Get(id);
            if (pizza == null)
            {
                return NotFound();
            }

            pizzaRepository.Delete(pizza);
            
            return RedirectToAction("Index");
        }
    }
}
