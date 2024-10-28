using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.IService;
using Service.Service;

namespace KoiShowManagementSystem.Pages.Koi
{
    public class DeleteKoiModel : PageModel
    {
        private readonly IKoiService _koiService;
        private readonly IAuthService _authService;
        public int KoiId { get; set; }
        public DeleteKoiModel(IKoiService koiService, IAuthService authService)
        {
            _koiService = koiService;
            _authService = authService;
        }

        public Repository.Entities.KoiFish KoiFish { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole == "Member")
            {
                var getKoi = await _koiService.GetKoiFishByUserIdAsync((int)userId);
                var koi = await _koiService.GetKoiById(id);

                if (koi == null)
                {
                    return NotFound();
                }
                KoiId = id;
                return Page();
            }
            TempData["ErrorMessage"] = "You don't have permission to access this page";
            await _authService.ClearSession();
            return RedirectToPage("/Authen/Login");
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var result = await _koiService.DeleteKoi(id);
            if (result.Code == 0)
            {
                TempData["SuccessMessage"] = result.Message;
                return RedirectToPage("/Koi/MyKoiList");
            }
            TempData["ErrorMessage"] = result.Message;
            return Page();
            
        }
        public async Task<IActionResult> OnPostLogout()
        {
            await _authService.ClearSession();
            return RedirectToPage("/Index");
        }
    }
}
