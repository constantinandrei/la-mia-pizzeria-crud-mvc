using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models.Form;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;

namespace la_mia_pizzeria_static.Models.Repositories
{
    public class DbPizzaRepository : IDbPizzaRepository
    {
        private PizzaDbContext db;
        private DbIngredientRepository ingredientRepository;
        private DbCategoryRepository categoryRepository;

        public DbPizzaRepository()
        {
            db = PizzaDbContext.GetInstance;
            ingredientRepository = new DbIngredientRepository();
            categoryRepository = new DbCategoryRepository();
        }
        public bool Exists(int id)
        {
            return db.Pizzas.Any(p => p.Id == id);
        }
        public List<Pizza> Get()
        {
            return db.Pizzas.Include(p => p.Category).Include(p => p.Ingredients).ToList();
        }

        public Pizza Get(int id)
        {
            return db.Pizzas.Where(p => p.Id == id).Include(p => p.Category).FirstOrDefault();
        }

        public List<Pizza> GetByCategoryId(int categoryId)
        {
            return db.Pizzas.Where(p => p.CategoryId == categoryId).Include(p => p.Category).ToList();
        }

        // crea il model vuoto con i dati da dinviare alla view
        public PizzaForm CreateForm()
        {
            PizzaForm formData = new PizzaForm();
            formData.Pizza = new Pizza();
            formData.Categories = categoryRepository.Get();
            formData.Ingredients = ingredientRepository.SelectList();

            return formData;
        }

        // riaggiunge da database i dati di ingredienti e categorie mantenendo i vecchi dati della pizza
        public PizzaForm CreateForm(PizzaForm formData)
        {
            formData.Categories = categoryRepository.Get();
            formData.Ingredients = new List<SelectListItem>();


            return formData;
        }

        // crea un form data con una pizza da database
        public PizzaForm CreateForm(int id)
        {
            PizzaForm formData = CreateForm();
            formData.Pizza = Get(id);
            List<Ingredient> IngredientsList = ingredientRepository.Get();
            foreach (Ingredient ingredient in IngredientsList)
            {
                bool selected = formData.Pizza.Ingredients.Any(i => i.Id == ingredient.Id);
                formData.Ingredients.Add(new SelectListItem(ingredient.Name, ingredient.Id.ToString(), selected));
            }
            return formData;
        }

        public void Create(Pizza pizza, List<int> selectedIngredients)
        {
            pizza.Ingredients = new List<Ingredient>();
            foreach (int ingredientId in selectedIngredients)
            {
                pizza.Ingredients.Add(ingredientRepository.Get(ingredientId));
            }
            db.Pizzas.Add(pizza);
            db.SaveChanges();
        }

        public void Update(Pizza updatedPizza, List<int> selectedIngredients)
        {
            Pizza pizza = Get(updatedPizza.Id);
            pizza.Name = updatedPizza.Name;
            pizza.Image = updatedPizza.Image;
            pizza.Price = updatedPizza.Price;
            pizza.Description = updatedPizza.Description;
            pizza.CategoryId = updatedPizza.CategoryId;
            if (pizza.Ingredients != null)
                pizza.Ingredients.Clear();
            ;
            if (selectedIngredients != null)
            {
                foreach (int ingredientId in selectedIngredients)
                {
                    pizza.Ingredients.Add(ingredientRepository.Get(ingredientId));

                }
            }

            db.SaveChanges();
        }

        public void Delete(Pizza pizza)
        {
            db.Pizzas.Remove(pizza);
            db.SaveChanges();
        }
    }
}
