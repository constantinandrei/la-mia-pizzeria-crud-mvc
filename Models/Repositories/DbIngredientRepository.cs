using la_mia_pizzeria_static.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.SqlServer.Server;

namespace la_mia_pizzeria_static.Models.Repositories
{
    public class DbIngredientRepository : IDbIngredientRepository
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

        public List<SelectListItem> SelectList()
        {
            List<SelectListItem> Ingredients = new List<SelectListItem>();
            List<Ingredient> IngredientsList = Get();
            foreach (Ingredient ingredient in IngredientsList)
            {
                Ingredients.Add(new SelectListItem(ingredient.Name, ingredient.Id.ToString()));
            }

            return Ingredients;
        }
    }
}
