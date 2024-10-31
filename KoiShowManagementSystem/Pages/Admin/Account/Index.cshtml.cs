using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Entities;
using Service.IService;

namespace KoiShowManagementSystem.Pages.Admin.Account
{
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public IndexModel(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        public IList<Repository.Entities.User> User { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole == "Admin")
            {
                User = await _userService.GetAll();
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
