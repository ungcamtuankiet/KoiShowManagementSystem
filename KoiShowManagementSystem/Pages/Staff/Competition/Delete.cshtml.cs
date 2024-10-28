using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service.IService;
using Service.Service;

namespace KoiShowManagementSystem.Pages.Staff.Competition
{
    public class DeleteModel : PageModel
    {
        private readonly ICompetitionService _competitionService;
        private readonly IAuthService _authService;

        public DeleteModel(ICompetitionService competitionService, IAuthService authService)
        {
            _competitionService = competitionService;
            _authService = authService;
        }

        [BindProperty]
        public Repository.Entities.Competition Competition { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole == "Staff")
            {
                if (id == null)
                {
                    return NotFound();
                }

                var competition = await _competitionService.GetCompetitionById((int)id);

                if (competition == null)
                {
                    return NotFound();
                }
                else
                {
                    Competition = competition;
                }
                return Page();
            }
            TempData["ErrorMessage"] = "You don't have permission to access this page";
            await _authService.ClearSession();
            return RedirectToPage("/Authen/Login");
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competition = await _competitionService.GetCompetitionById((int)id);
            if (competition != null)
            {
                Competition = competition;
                var result = await _competitionService.DeleteCompetition(Competition);
                if (result.Code == 0)
                {
                    TempData["SuccessMessage"] = result.Message;
                    return RedirectToPage("/Staff/Competition/Index");
                }
                TempData["ErrorMessage"] = result.Message;
                return Page();
            }
            return RedirectToPage("./Index");
        }
        public async Task<IActionResult> OnPostLogout()
        {
            await _authService.ClearSession();
            return RedirectToPage("/Index");
        }
    }
}
