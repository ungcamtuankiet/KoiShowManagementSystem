using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace KoiShowManagementSystem.Pages
{
    public class IndexModel : PageModel
    {
        public string? UserRole { get; private set; }

        public void OnGet()
        {
            // L?y thông tin vai trò Session
            UserRole = HttpContext.Session.GetString("UserRole");
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Index");
        }
    }
}