using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.IService;

namespace KoiShowManagementSystem.Pages.Staff.ManageKoiRegistration
{
    public class RejectKoiRegistrationModel : PageModel
    {
        private readonly IKoiRegistrationService _koiRegistrationService;
        private readonly IAuthService _authService;

        public RejectKoiRegistrationModel(IKoiRegistrationService koiRegistrationService, IAuthService authService)
        {
            _koiRegistrationService = koiRegistrationService;
            _authService = authService;
        }
        public int CompetitionId { get; set; }
        public int KoiRegistrationId { get; set; }
        public string Reason { get; set; }
        public async Task<IActionResult> OnGetAsync(int koiRegistrationId, int competitionId)
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole == "Staff")
            {
                CompetitionId = competitionId;
                KoiRegistrationId = koiRegistrationId;
                return Page();
            }
            TempData["ErrorMessage"] = "You don't have permission to access this page";
            await _authService.ClearSession();
            return RedirectToPage("/Authen/Login");
        }
        public async Task<IActionResult> OnPostAsync(int koiRegistrationId, int competitionId, string reason)
        {
            Reason = reason;
            var response = await _koiRegistrationService.RejectKoiRegistration(koiRegistrationId, Reason);
            if (response.Code == 1)
            {
                TempData["ErrorMessage"] = response.Message;
                return RedirectToPage("/Staff/ManageKoiRegistration/Index");
            }
            TempData["SuccessMessage"] = response.Message;
            return RedirectToPage("/Staff/ManageKoiRegistration/Index");
        }
        public async Task<IActionResult> OnPostLogout()
        {
            await _authService.ClearSession();
            return RedirectToPage("/Index");
        }
    }
}
