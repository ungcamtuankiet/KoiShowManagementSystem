using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.IService;

namespace KoiShowManagementSystem.Pages.Koi
{
    public class DeleteKoiModel : PageModel
    {
        private readonly IKoiService _koiService;

        public DeleteKoiModel(IKoiService koiService)
        {
            _koiService = koiService;
        }

        public Repository.Entites.KoiFish KoiFish { get; set; } = default!;

/*        public async Task<IActionResult> OnPostAsync()
        {
            var result = await _koiService.DeleteKoi(KoiFish);
            if(result.Code == 0)
            {
                TempData["SuccessMessage"] = result.Message;
                return Page();
            }
            TempData["ErrorMessage"] = result.Message;
            return Page();
        }*/
    }
}
