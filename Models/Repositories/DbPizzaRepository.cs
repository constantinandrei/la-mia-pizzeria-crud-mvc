using la_mia_pizzeria_static.Data;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Models.Repositories
{
    public class DbPizzaRepository
    {
        private PizzaDbContext db;
        public DbPizzaRepository()
        {
            db = new PizzaDbContext();
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
    }
}
