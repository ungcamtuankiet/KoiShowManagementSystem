using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KoiShowManagementSystem.Pages.Staff
{
    public class StaffPageModel : PageModel
    {
        public string? UserRole { get; private set; }
        public void OnGet()
        {
            // L?y th�ng tin role t? Session
            UserRole = HttpContext.Session.GetString("UserRole");
        }

        public IActionResult OnPostLogout()
        {
            // X�a t?t c? th�ng tin trong Session v� chuy?n v? trang login
            HttpContext.Session.Clear();
            return RedirectToPage("/Authen/Login");
        }
    }
}
