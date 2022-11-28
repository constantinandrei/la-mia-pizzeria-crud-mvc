using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models.Form;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace la_mia_pizzeria_static.Models.Repositories
{
    public class ListPizzaRepository : IDbPizzaRepository
    {
        private IDbIngredientRepository ingredientRepository;
        private IDbCategoryRepository categoryRepository;
        private static List<Pizza> _pizzas;

        public ListPizzaRepository()
        {
            ingredientRepository = new ListIngredientRepository();
            categoryRepository = new ListCategoryRepository();
            if (_pizzas == null)
            {
                _pizzas = new List<Pizza>();


                Pizza pizza = new Pizza();
                pizza.Id = 1;
                pizza.Price = 7.5;
                pizza.Description = "ha dka ala dha ds";
                pizza.Category = categoryRepository.Get(1);
                pizza.Ingredients = new List<Ingredient>();
                pizza.Ingredients.Add(ingredientRepository.Get(1));
                pizza.Image = "https://picsum.photos/200/300";
                _pizzas.Add(pizza);
            }
                
            
        }
        public void Create(Pizza pizza, List<int> selectedIngredients)
        {
            pizza.Category = categoryRepository.Get(pizza.CategoryId);
            pizza.Ingredients = new List<Ingredient>();
            if (selectedIngredients != null)
            {
                foreach (int ingredientId in selectedIngredients)
                {
                    pizza.Ingredients.Add(ingredientRepository.Get(ingredientId));
                }
            }
            
            if (_pizzas.Count() == 0)
            {
                pizza.Id = 1;
            } else
            {
                pizza.Id = _pizzas.Last().Id + 1;
            }
            
            _pizzas.Add(pizza);
        }

        public PizzaForm CreateForm()
        {
            PizzaForm formData = new PizzaForm();
            formData.Pizza = new Pizza();
            formData.Categories = categoryRepository.Get();
            formData.Ingredients = ingredientRepository.SelectList();

            return formData;
        }

        public PizzaForm CreateForm(int id)
        {
            PizzaForm formData = CreateForm();
            formData.Pizza = Get(id);
            formData.Ingredients.Clear();
            foreach (Ingredient ingredient in ingredientRepository.Get())
            {
                bool selected = formData.Pizza.Ingredients.Any(i => i.Id == ingredient.Id);
                formData.Ingredients.Add(new SelectListItem(ingredient.Name, ingredient.Id.ToString(), selected));
            }
            return formData;
        }

        public PizzaForm CreateForm(PizzaForm formData)
        {
            formData.Categories = categoryRepository.Get();
            formData.Ingredients = new List<SelectListItem>();


            return formData;
        }

        public void Delete(Pizza pizza)
        {
            Pizza toDelete = Get(pizza.Id);
            _pizzas.Remove(toDelete);
        }

        public bool Exists(int id)
        {
            return _pizzas.Any(p => p.Id == id);
        }

        public List<Pizza> Get()
        {
            return _pizzas;
        }

        public Pizza Get(int id)
        {
            return _pizzas.FirstOrDefault(p => p.Id == id);
        }

        public List<Pizza> GetByCategoryId(int categoryId)
        {
            return _pizzas.Where(p => p.CategoryId == categoryId).ToList();
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
        }
    }
}
