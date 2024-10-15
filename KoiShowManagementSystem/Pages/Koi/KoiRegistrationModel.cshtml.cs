using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Dtos.Koi;
using Repository.Entites;
using Service.IService;

namespace KoiShowManagementSystem.Pages.Koi
{
    public class KoiRegistrationModelModel : PageModel
    {
        private readonly IKoiService _koiService;

        public KoiRegistrationModelModel(IKoiService koiService)
        {
            _koiService = koiService;
        }
        [BindProperty]
        public int KoiId { get; set; }
        [BindProperty]
        public int CompetitionId { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await _koiService.RegisterKoi(KoiId, CompetitionId);
                SuccessMessage = "Koi registered successfully!";
                return Page();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return Page();
            }
        }
    }
}
