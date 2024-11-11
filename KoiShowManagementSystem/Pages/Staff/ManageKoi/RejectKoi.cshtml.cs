using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.IService;

namespace KoiShowManagementSystem.Pages.Staff.ManageKoi
{
    public class RejectKoiModel : PageModel
    {
        private readonly IKoiService? _koiService;
        private readonly IAuthService _authService;

        public RejectKoiModel(IKoiService? koiService, IAuthService authService)
        {
            _koiService = koiService;
            _authService = authService;
        }
        public string Reason { get; set; }
        public async Task<IActionResult> OnPostAsync(int id, string reason)
        {
            var response = await _koiService.Reject(id, reason);
            if (response.Code == 1)
            {
                TempData["ErrorMessage"] = response.Message;
                return RedirectToPage("/Staff/ManageKoi/Index");
            }
            TempData["SuccessMessage"] = response.Message;
            return RedirectToPage("/Staff/ManageKoi/Index");
        }
        public async Task<IActionResult> OnPostLogout()
        {
            await _authService.ClearSession();
            return RedirectToPage("/Index");
        }
    }
}
