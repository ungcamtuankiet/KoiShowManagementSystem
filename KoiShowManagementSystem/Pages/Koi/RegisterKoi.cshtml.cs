using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.Dtos.Koi;
using Repository.Entites;
using Repository.Enum;
using Service.IService;
using Service.Service;

namespace KoiShowManagementSystem.Pages.Koi
{
    public class RegisterKoiModel : PageModel
    {
        private readonly IKoiService _koiService;
        private readonly IAuthService _authService;

        public RegisterKoiModel(IKoiService koiService, IAuthService authService)
        {
            _koiService = koiService;
            _authService = authService;
        }

        [BindProperty]
        public RegisterKoi KoiDto { get; set; } = new RegisterKoi();
        public IEnumerable<SelectListItem> koiVarieties { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole == "Member")
            {
                koiVarieties = Enum.GetValues(typeof(KoiVariety))
                               .Cast<KoiVariety>()
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

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                TempData["ErrorMessage"] = "User is not logged in.";
                return Page(); 
            }

            var response = await _koiService.RegisterKoi(KoiDto, userId);
            if (response.Code != 0)
            {
                koiVarieties = Enum.GetValues(typeof(KoiVariety))
                               .Cast<KoiVariety>()
                               .Select(v => new SelectListItem
                               {
                                   Value = v.ToString(),
                                   Text = v.ToString()
                               })
                               .ToList();
                TempData["ErrorMessage"] = response.Message;
                return Page();
            }
            TempData["SuccessMessage"] = response.Message;
            return RedirectToPage("/Koi/MyKoiList");
        }
        public async Task<IActionResult> OnPostLogout()
        {
            await _authService.ClearSession();
            return RedirectToPage("/Index");
        }
    }
}
