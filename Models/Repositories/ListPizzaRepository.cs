using la_mia_pizzeria_static.Models.Form;

namespace la_mia_pizzeria_static.Models.Repositories
{
    public class ListPizzaRepository : IDbPizzaRepository
    {
        public void Create(Pizza pizza, List<int> selectedIngredients)
        {
            throw new NotImplementedException();
        }

        public PizzaForm CreateForm()
        {
            throw new NotImplementedException();
        }

        public PizzaForm CreateForm(int id)
        {
            throw new NotImplementedException();
        }

        public PizzaForm CreateForm(PizzaForm formData)
        {
            throw new NotImplementedException();
        }

        public void Delete(Pizza pizza)
        {
            throw new NotImplementedException();
        }

        public bool Exists(int id)
        {
            throw new NotImplementedException();
        }

        public List<Pizza> Get()
        {
            throw new NotImplementedException();
        }

        public Pizza Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Pizza> GetByCategoryId(int categoryId)
        {
            throw new NotImplementedException();
        }

        public void Update(Pizza updatedPizza, List<int> selectedIngredients)
        {
            throw new NotImplementedException();
        }
    }
}
