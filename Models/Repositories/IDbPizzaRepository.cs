using la_mia_pizzeria_static.Models.Form;

namespace la_mia_pizzeria_static.Models.Repositories
{
    public interface IDbPizzaRepository
    {
        void Create(Pizza pizza, List<int> selectedIngredients);
        PizzaForm CreateForm();
        PizzaForm CreateForm(int id);
        PizzaForm CreateForm(PizzaForm formData);
        void Delete(Pizza pizza);
        bool Exists(int id);
        List<Pizza> Get();
        Pizza Get(int id);
        List<Pizza> GetByCategoryId(int categoryId);
        void Update(Pizza updatedPizza, List<int> selectedIngredients);
    }
}