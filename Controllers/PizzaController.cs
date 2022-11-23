using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models;
using la_mia_pizzeria_static.Models.Form;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.SqlServer.Server;
using System.Data;

namespace la_mia_pizzeria_static.Controllers
{
    
    public class PizzaController : Controller
    {
        private PizzaDbContext db;

        public PizzaController()
        {
            db = new PizzaDbContext();
        }
            public IActionResult Index()
        {
           
            List<Pizza> Pizze = db.Pizzas.Include(p => p.Category).ToList();
            ViewData["Title"] = "Todi Pizza";
            return View(Pizze);
        }

        public IActionResult Detail(int id)
        {
            Pizza pizza = db.Pizzas.Where(p => p.Id == id).Include(p => p.Category).FirstOrDefault();
            ViewData["Title"] = "Todi Pizza | " + pizza.Name;
            return View(pizza);
        }
        public IActionResult Create()
        {
            PizzaForm formData = new PizzaForm();
            formData.Pizza = new Pizza();
            formData.Categories = db.Categories.ToList();
            return View(formData);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaForm formData)
        {
            if (!ModelState.IsValid)
            {
                formData.Categories = db.Categories.ToList();
                return View(formData);
            }

            db.Pizzas.Add(formData.Pizza);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            PizzaForm formData = new PizzaForm();
            formData.Pizza = db.Pizzas.Where(p => p.Id == id).FirstOrDefault();
            if (formData.Pizza == null)
                return NotFound();
            formData.Categories = db.Categories.ToList();
            return View(formData);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, PizzaForm formData)
        {
            if (!ModelState.IsValid)
            {
                formData.Categories = db.Categories.ToList();
                return View(formData);
            }

            formData.Pizza.Id = id;
            db.Pizzas.Update(formData.Pizza);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Pizza pizza = db.Pizzas.Where(p => p.Id == id).FirstOrDefault();
            if (pizza == null)
            {
                return NotFound();
            }

            db.Pizzas.Remove(pizza);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
