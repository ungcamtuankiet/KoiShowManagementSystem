using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KoiShowManagementSystem.Pages.Staff
{
    public class StaffPageModel : PageModel
    {
        public string? UserRole { get; private set; }

        public void OnGet()
        {
            UserRole = HttpContext.Session.GetString("UserRole");
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Index");
        }
    }
}
