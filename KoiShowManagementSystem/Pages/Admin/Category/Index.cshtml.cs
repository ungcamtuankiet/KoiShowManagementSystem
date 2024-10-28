using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.IService;
using Service.Service;

namespace KoiShowManagementSystem.Pages.Category
{
    public class IndexModel : PageModel
    {
        private readonly ICategoryService _categoryService;
        private readonly IAuthService _authService;

        public IEnumerable<Repository.Entities.Category> Categories { get; set; }
        public string? UserRole { get; private set; }

        public IndexModel(ICategoryService categoryService, IAuthService authService)
        {
            _categoryService = categoryService;
            _authService = authService;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole == "Admin")
            {
                var listCategory = await _categoryService.GetCategoriesAsync();
                Categories = listCategory;
                UserRole = await _authService.GetUserRole(UserRole);
                return Page();
            }
            TempData["ErrorMessage"] = "You don't have permission to access this page";
            await _authService.ClearSession();
            return RedirectToPage("/Authen/Login");
        }
        public async Task<IActionResult> OnPostLogout()
        {
            await _authService.ClearSession();
            return RedirectToPage("/Index");
        }
    }
}
