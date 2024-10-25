using Repository.Dtos.Category;
using Repository.Dtos.Response;
using Repository.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryById(int id);
        Task<Response> CreateCategory(CreateCategory createCategory);
        Task<Response> UpdateCategory(UpdateCategory updateCategory, int id);
        Task<Response> DeleteCategory(int id);
    }
}
