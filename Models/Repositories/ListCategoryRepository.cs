namespace la_mia_pizzeria_static.Models.Repositories
{
    public class ListCategoryRepository : IDbCategoryRepository
    {
        List<Category> _categories;
        public ListCategoryRepository()
        {
            _categories = new List<Category>();
            _categories.Add(new Category(1, "Classiche"));
            _categories.Add(new Category(1, "Speciali"));
            _categories.Add(new Category(1, "Crude"));

        }
        public void Create(Category category)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Exists(int id)
        {
            throw new NotImplementedException();
        }

        public List<Category> Get()
        {
            throw new NotImplementedException();
        }

        public Category Get(int id)
        {
            throw new NotImplementedException();
        }

        public bool HasConstraint(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
