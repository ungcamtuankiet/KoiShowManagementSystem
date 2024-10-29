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
using Service.IService;
using Service.Service;

namespace KoiShowManagementSystem.Pages.Staff.ManageKoiRegistration
{
    public class IndexModel : PageModel
    {
        private readonly IKoiRegistrationService _koiRegistrationService;
        private readonly IAuthService _authService;

        public IndexModel(IKoiRegistrationService koiRegistrationService, IAuthService authService)
        {
            _koiRegistrationService = koiRegistrationService;
            _authService = authService;
        }

        public IList<KoiRegistration> KoiRegistration { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole == "Staff")
            {
                KoiRegistration = await _koiRegistrationService.GetListKoiRegistrationAsync();
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
