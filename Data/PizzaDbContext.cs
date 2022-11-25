using la_mia_pizzeria_static.Models;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Data
{
    public class PizzaDbContext : DbContext
    {
        public static PizzaDbContext GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PizzaDbContext();
                }
                return _instance;
            }
        }

        private static PizzaDbContext _instance;
        private PizzaDbContext()
        {
        }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=la-mia-pizzeria;Integrated Security=True;Encrypt=False");
        }
    }
}
