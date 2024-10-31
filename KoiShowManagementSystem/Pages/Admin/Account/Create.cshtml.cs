using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository;
using Repository.Entities;
using Repository.Enum;
using Service.IService;

namespace KoiShowManagementSystem.Pages.Admin.Account
{
    public class CreateModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public CreateModel(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }
        public IEnumerable<SelectListItem> UserRoles { get; set; }
        public async Task<IActionResult> OnGet()
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole == "Admin")
            {
                UserRoles = Enum.GetValues(typeof(RoleEnum))
                               .Cast<RoleEnum>()
                               .Select(v => new SelectListItem
                               {
                                   Value = v.ToString(),
                                   Text = v.ToString()
                               })
                               .ToList();
                return Page();
            }
            TempData["ErrorMessage"] = "You don't have permission to access this page";
            await _authService.ClearSession();
            return RedirectToPage("/Authen/Login");
        }

        [BindProperty]
        public Repository.Entities.User User { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var result = await _userService.AddNewUser(User);
            if(result.Code == 1)
            {
                UserRoles = Enum.GetValues(typeof(RoleEnum))
                               .Cast<RoleEnum>()
                               .Select(v => new SelectListItem
                               {
                                   Value = v.ToString(),
                                   Text = v.ToString()
                               })
                               .ToList();
                TempData["ErrorMessage"] = result.Message;
                return Page();
            }
            TempData["SuccessMessage"] = result.Message;
            return RedirectToPage("./Index");
        }
        public async Task<IActionResult> OnPostLogout()
        {
            await _authService.ClearSession();
            return RedirectToPage("/Index");
        }
    }
}
