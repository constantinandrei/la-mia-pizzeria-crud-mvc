using Microsoft.AspNetCore.Mvc.Rendering;

namespace la_mia_pizzeria_static.Models.Repositories
{
    public class ListIngredientRepository : IDbIngredientRepository
    {
        private static List<Ingredient> _ingredients;
        public ListIngredientRepository()
        {
            if (_ingredients == null)
            {
                _ingredients = new List<Ingredient>();
                _ingredients.Add(new Ingredient(1, "Farina"));
                _ingredients.Add(new Ingredient(2, "Pomodoro"));
                _ingredients.Add(new Ingredient(3, "Mozzarella"));
                _ingredients.Add(new Ingredient(4, "Mortadella"));
                _ingredients.Add(new Ingredient(5, "Amore"));
            }
            

        }
        public List<Ingredient> Get()
        {
            return _ingredients;
        }

        public Ingredient Get(int id)
        {
            return _ingredients.Where(i => i.Id == id).FirstOrDefault();
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
