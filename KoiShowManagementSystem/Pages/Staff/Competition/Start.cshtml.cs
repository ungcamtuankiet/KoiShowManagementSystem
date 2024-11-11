using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service.IService;
using Service.Service;

namespace KoiShowManagementSystem.Pages.Staff.Competition
{
    public class StartModel : PageModel
    {
        private readonly IAuthService _authService;
        private readonly ICompetitionService _competitionService;

        public StartModel(IAuthService authService, ICompetitionService competitionService)
        {
            _authService = authService;
            _competitionService = competitionService;
        }
        [BindProperty]
        public int CompetitionId { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            // L?u id t? route vào CompetitionId
            CompetitionId = id;

            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole == "Staff")
            {
                return Page();
            }
            TempData["ErrorMessage"] = "You don't have permission to access this page";
            await _authService.ClearSession();
            return RedirectToPage("/Authen/Login");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            // B?t ??u cu?c thi
            var response = await _competitionService.StartCompetition(CompetitionId);
            if (response.Code != 0)
            {
                TempData["ErrorMessage"] = response.Message;
                return Page();
            }

            TempData["SuccessMessage"] = "B?t ??u cu?c thi thành công";
            return RedirectToPage("/Staff/Competition/Details", new { id = CompetitionId });
        }
        public async Task<IActionResult> OnPostLogout()
        {
            await _authService.ClearSession();
            return RedirectToPage("/Index");
        }
    }
}
