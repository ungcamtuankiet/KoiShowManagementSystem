using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.IService;
using Service.Service;

namespace KoiShowManagementSystem.Pages.Category
{
    public class DeleteModel : PageModel
    {
        private readonly ICategoryService _categoryService;
        private readonly IAuthService _authService;

        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public string? UserRole { get; private set; }
        public DeleteModel(ICategoryService categoryService, IAuthService authService)
        {
            _categoryService = categoryService;
            _authService = authService;
        }

        // Get category details to display on delete confirmation page
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole == "Member")
            {
                var response = await _categoryService.GetCategoriesAsync();
                var category = await _categoryService.GetCategoryById(id);

                if (category == null)
                {
                    return NotFound();
                }

                CategoryName = category.Name;
                CategoryId = id;

                return Page();
            }
            TempData["ErrorMessage"] = "You don't have permission to access this page";
            await _authService.ClearSession();
            return RedirectToPage("/Authen/Login");
        }

        // Handle category deletion
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var response = await _categoryService.DeleteCategory(id);
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
