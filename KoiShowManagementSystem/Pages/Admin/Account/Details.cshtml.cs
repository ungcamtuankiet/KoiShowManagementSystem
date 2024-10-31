using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Entities;
using Repository.Enum;
using Service.IService;

namespace KoiShowManagementSystem.Pages.Admin.Account
{
    public class DetailsModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public DetailsModel(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        public Repository.Entities.User User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole == "Admin")
            {
                if (id == null)
                {
                    return NotFound();
                }

                var user = await _userService.GetUserById(id);
                if (user == null)
                {
                    return NotFound();
                }
                else
                {
                    User = user;
                }
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
