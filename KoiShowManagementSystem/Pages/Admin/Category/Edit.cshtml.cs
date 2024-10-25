using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Dtos.Category;
using Repository.Enum;
using Service.IService;

namespace KoiShowManagementSystem.Pages.Category
{
    public class EditModel : PageModel
    {
        private readonly ICategoryService _categoryService;
        private readonly IAuthService _authService;

        [BindProperty]
        public UpdateCategory Category { get; set; }
        public string? UserRole { get; private set; }
        public EditModel(ICategoryService categoryService, IAuthService authService)
        {
            _categoryService = categoryService;
            _authService = authService;
        }
        public int Id { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {

            var userRole = await _authService.GetUserRole(UserRole);
            UserRole = await _authService.GetUserRole("AdminRole");
            if (userRole == "Admin")
            {
                var category = await _categoryService.GetCategoryById(id);

                if (category == null)
                {
                    return NotFound();
                }
                Category = new UpdateCategory()
                {
                    Name = category.Name,
                    Status = category.Status
                };

                return Page();
            }
            TempData["ErrorMessage"] = "You don't have permission to access this page";
            await _authService.ClearSession();
            return RedirectToPage("/Authen/Login");
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var response = await _categoryService.UpdateCategory(Category, id);
            if (response.Code == 0)
            {
                TempData["SuccessMessage"] = response.Message;
                return RedirectToPage("/Admin/Category/Index");
            }

            TempData["ErrorMessage"] = response.Message;
            return Page();
        }
        public async Task<IActionResult> OnPostLogout()
        {
            await _authService.ClearSession();
            return RedirectToPage("/Index");
        }
    }
}
