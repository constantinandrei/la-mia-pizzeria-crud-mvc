using Microsoft.AspNetCore.Mvc.Rendering;

namespace la_mia_pizzeria_static.Models.Repositories
{
    public interface IDbIngredientRepository
    {
        List<Ingredient> Get();
        Ingredient Get(int id);
        List<SelectListItem> SelectList();
    }
}