using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.IService;

namespace KoiShowManagementSystem.Pages.Admin
{
    public class AdminPageModel : PageModel
    {
        private readonly IAuthService _authService;

        public AdminPageModel(IAuthService authService)
        {
            _authService = authService;
        }

        public string? UserRole { get; private set; }

        public async Task<IActionResult> OnGet()
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole == "Admin")
            {
                UserRole = await _authService.GetUserRole("AdminRole");
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
