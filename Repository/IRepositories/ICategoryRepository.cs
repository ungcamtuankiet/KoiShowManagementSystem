using Repository.Entities;

namespace Repository.IRepositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategoryById(int id);
        Task<Category> GetCategoryByName(string name);
        Task<bool> GetCategoryCurrent(string name, int id);
        Task CreateCategory(Category category);
        Task UpdateCategory(Category category);
        Task DeleteCategory(Category category);
    }
}

