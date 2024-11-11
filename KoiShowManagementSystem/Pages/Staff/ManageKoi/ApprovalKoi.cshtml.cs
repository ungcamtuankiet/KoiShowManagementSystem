using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Entities;
using Service.IService;
using Service.Service;

namespace KoiShowManagementSystem.Pages.Staff.ManageKoi
{
    public class ApprovalKoiModel : PageModel
    {
        private readonly IKoiService? _koiService;
        private readonly IAuthService _authService;

        public ApprovalKoiModel(IKoiService? koiService, IAuthService authService)
        {
            _koiService = koiService;
            _authService = authService;
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var response = await _koiService.ApprovalKoi(id);
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
