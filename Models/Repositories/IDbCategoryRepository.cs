namespace la_mia_pizzeria_static.Models.Repositories
{
    public interface IDbCategoryRepository
    {
        void Create(Category category);
        void Delete(int id);
        bool Exists(int id);
        List<Category> Get();
        Category Get(int id);
        bool HasConstraint(int id);
        void Update(Category category);
    }
}