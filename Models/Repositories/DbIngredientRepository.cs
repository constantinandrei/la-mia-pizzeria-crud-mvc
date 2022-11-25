using la_mia_pizzeria_static.Data;

namespace la_mia_pizzeria_static.Models.Repositories
{
    public class DbIngredientRepository
    {
        private PizzaDbContext db;
        public DbIngredientRepository()
        {
            db = PizzaDbContext.GetInstance;
        }

        public List<Ingredient> Get()
        {
           return db.Ingredients.ToList();
        }

        public Ingredient Get(int id)
        {
            return db.Ingredients.Where(i => i.Id == id).FirstOrDefault();
        }
    }
}
