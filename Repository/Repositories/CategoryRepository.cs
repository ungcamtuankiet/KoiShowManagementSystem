using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.IRepositories;


namespace Repository.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly KoiShowManagementSystemContext _context;

        public CategoryRepository(KoiShowManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }public async Task<Category> GetCategoryByName(string name)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Name == name);
        }
        public async Task<bool> GetCategoryCurrent(string name, int id)
        {
            return await _context.Categories.AnyAsync(u => u.Name == name && u.Id != id);
        }
        public async Task CreateCategory(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteCategory(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}
