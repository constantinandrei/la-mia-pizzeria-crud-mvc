using la_mia_pizzeria_static.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;

namespace la_mia_pizzeria_static.Models.Repositories
{
    public class DbPizzaRepository
    {
        private PizzaDbContext db;
        private DbIngredientRepository ingredientRepository;

        public DbPizzaRepository()
        {
            db = PizzaDbContext.GetInstance;
            ingredientRepository = new DbIngredientRepository();
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
