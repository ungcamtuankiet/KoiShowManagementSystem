using Repository;
using Repository.Data;
using Repository.Dtos.Category;
using Repository.Dtos.Response;
using Repository.Entities;
using Repository.Enum;
using Repository.IRepositories;
using Service.IService;

namespace Service.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            var getCategories = await _repository.GetCategories();
            return getCategories;
        }

        public async Task<Category> GetCategoryById(int id)
        {
            var getCategory = await _repository.GetCategoryById(id);
            return getCategory;
        }
        public async Task<Response> CreateCategory(CreateCategory createCategory)
        {
            var getCategory = await _repository.GetCategoryByName(createCategory.Name);
            if(getCategory != null)
            {
                return new Response()
                {
                    Code = 1,
                    Message = "Category already exist",
                    Data = null
                };
            }
            var newCategory = new Category()
            {
                Name = createCategory.Name,
                Status = CategoryStatus.Active.ToString(),
                CreatedAt = DateTime.Now,
            };
            await _repository.CreateCategory(newCategory);
            return new Response()
            {
                Code = 0,
                Message = "Create Category Successfully",
                Data = null
            };
        }

        public async Task<Response> UpdateCategory(UpdateCategory updateCategory, int id)
        {
            var getCategory = await _repository.GetCategoryByName(updateCategory.Name);
            var checkCategoryName = await _repository.GetCategoryCurrent(updateCategory.Name, id);
            var getCateogryById = await _repository.GetCategoryById(id);
            if (checkCategoryName) return new Response() { Code = 1, Message = "Category Name Alredy exist", Data = null };
            getCateogryById.Name = updateCategory.Name;
            getCateogryById.UpdatedAt = DateTime.Now;
            getCateogryById.Status = updateCategory.Status;
            await _repository.UpdateCategory(getCateogryById);
            return new Response()
            {
                Code = 0,
                Message = "Update Category Successfully",
                Data = null
            };
        }

        public async Task<Response> DeleteCategory(int id)
        {
            var getCategory = await _repository.GetCategoryById(id);
            if (getCategory != null)
            {
                await _repository.DeleteCategory(getCategory);
                return new Response()
                {
                    Code = 0,
                    Message = "Delete Category Successfully",
                    Data = null
                };
            }
            return new Response()
            {
                Code = 1,
                Message = "Category not found",
                Data = null
            };
        }
    }
}
