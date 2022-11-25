using la_mia_pizzeria_static.Data;

namespace la_mia_pizzeria_static.Models.Repositories
{
    public class DbCategoryRepository : IDbCategoryRepository
    {
        private PizzaDbContext db;

        public DbCategoryRepository()
        {
            db = PizzaDbContext.GetInstance;
        }
        public bool Exists(int id)
        {
            return db.Categories.Any(c => c.Id == id);
        }
        public List<Category> Get()
        {
            return db.Categories.ToList();
        }

        public Category Get(int id)
        {
            return db.Categories.Where(c => c.Id == id).FirstOrDefault();
        }

        public void Create(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();
        }

        public void Update(Category category)
        {
            db.Categories.Update(category);
            db.SaveChanges();
        }
        public bool HasConstraint(int id)
        {
            Category category = Get(id);
            return category.Pizzas.Count() > 0;
        }
        public void Delete(int id)
        {
            Category category = Get(id);
            db.Categories.Remove(category);
            db.SaveChanges();
        }
    }
}
