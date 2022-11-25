namespace la_mia_pizzeria_static.Models.Repositories
{
    public class ListCategoryRepository : IDbCategoryRepository
    {
        static List<Category> _categories;
        public ListCategoryRepository()
        {
            _categories = new List<Category>();
            _categories.Add(new Category(1, "Classiche"));
            _categories.Add(new Category(1, "Speciali"));
            _categories.Add(new Category(1, "Crude"));

        }
        public void Create(Category category)
        {
            _categories.Add(category);
        }

        public void Delete(int id)
        {
            Category category = Get(id);
            _categories.Remove(category);
        }

        public bool Exists(int id)
        {
            return _categories.Any(c => c.Id == id);    
        }

        public List<Category> Get()
        {
            return _categories;
        }

        public Category Get(int id)
        {
            return _categories.FirstOrDefault(c => c.Id == id);
        }

        public bool HasConstraint(int id)
        {
            //da implementare, intanto metto sempre vero
            return true;
        }

        public void Update(Category category)
        {
            Category tuUpdate = Get(category.Id);
            tuUpdate.Name = category.Name;
        }
    }
}
