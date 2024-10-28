using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using Repository;
using Service.IService;

namespace KoiShowManagementSystem.Pages.Staff.Competition
{
    public class IndexModel : PageModel
    {
        private readonly ICompetitionService _competitionService;
        private readonly IAuthService _authService;
        private readonly ICategoryService _categoryService;

        public IndexModel(ICompetitionService competitionService, ICategoryService categoryService, IAuthService authService)
        {
            _competitionService = competitionService;
            _categoryService = categoryService;
            _authService = authService;
        }

        public IList<Repository.Entities.Competition> Competition { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole == "Staff")
            {
                Competition = await _competitionService.GetCompetitionList();
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
