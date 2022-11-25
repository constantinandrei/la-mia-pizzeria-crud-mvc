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
     
        private IDbPizzaRepository pizzaRepository;

        public PizzaController()
        {
            pizzaRepository = new ListPizzaRepository();
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
            
            return View(pizzaRepository.CreateForm());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaForm formData)
        {
            if (!ModelState.IsValid)
            {
                
                return View(pizzaRepository.CreateForm(formData));
            }

            pizzaRepository.Create(formData.Pizza, formData.SelectedIngredients);

            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            if (!pizzaRepository.Exists(id))
                return NotFound();
           
            return View(pizzaRepository.CreateForm(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, PizzaForm formData)
        {
            formData.Pizza.Id = id;
            if (!ModelState.IsValid)
            {
                return View(pizzaRepository.CreateForm(formData));
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
