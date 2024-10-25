using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Dtos.Category;
using Service.IService;

namespace KoiShowManagementSystem.Pages.Category
{ 
    public class CreateModel : PageModel
    {
        private readonly ICategoryService _categoryService;
        private readonly IAuthService _authService;

        [BindProperty]
        public CreateCategory Category { get; set; }

        public CreateModel(ICategoryService categoryService, IAuthService authService)
        {
            _categoryService = categoryService;
            _authService = authService;
        }
        public string? UserRole { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            UserRole = await _authService.GetUserRole("AdminRole");
            var userRole = HttpContext.Session.GetString("UserRole");
            if(userRole == "Admin")
            {
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var response = await _categoryService.CreateCategory(Category);
            if (response.Code == 0)
            {
                TempData["SuccessMessage"] = response.Message;
                return RedirectToPage("/Admin/Category/Index");
            }
            TempData["ErrorMessage"] = response.Message;
            return Page();
        }
    }
}
