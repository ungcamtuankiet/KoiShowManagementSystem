using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Dtos.Koi;
using Repository.Entites;
using Service.IService;

namespace KoiShowManagementSystem.Pages.Koi
{
    public class RegisterKoiModel : PageModel
    {
        private readonly IKoiService _koiService;

        public RegisterKoiModel(IKoiService koiService)
        {
            _koiService = koiService;
        }

        [BindProperty]
        public RegisterKoi KoiDto { get; set; } = new RegisterKoi();

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
                TempData["ErrorMessage"] = response.Message;
                return Page();
            }
            TempData["SuccessMessage"] = response.Message;
            return Page();
        }
    }
}
