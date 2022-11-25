using la_mia_pizzeria_static.Data;

namespace la_mia_pizzeria_static.Models.Repositories
{
    public class DbIngredientRepository
    {
        private PizzaDbContext db;
        public DbIngredientRepository()
        {
            db = new PizzaDbContext();
        }

        public List<Ingredient> Get()
        {
           return db.Ingredients.ToList();
        }

    }
}
