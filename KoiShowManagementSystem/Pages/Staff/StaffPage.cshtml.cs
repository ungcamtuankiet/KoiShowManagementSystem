using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KoiShowManagementSystem.Pages.Staff
{
    public class StaffPageModel : PageModel
    {
        public string? UserRole { get; private set; }
        public void OnGet()
        {
            // L?y thông tin role t? Session
            UserRole = HttpContext.Session.GetString("UserRole");
        }

        public IActionResult OnPostLogout()
        {
            // Xóa t?t c? thông tin trong Session và chuy?n v? trang login
            HttpContext.Session.Clear();
            return RedirectToPage("/Authen/Login");
        }
    }
}
