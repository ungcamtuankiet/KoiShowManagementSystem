using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.IService;

namespace KoiShowManagementSystem.Pages.Koi
{
    public class UpdateKoiModel : PageModel
    {
        private readonly IKoiService _service;

        public UpdateKoiModel(IKoiService service)
        {
            _service = service;
        }

        public Repository.Entites.KoiFish KoiFish { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            var result = await _service.UpdateKoi(KoiFish);
            if(result.Code == 0)
            {
                TempData["SuccessMessage"] = result.Message;
                return Page();  
            }
            TempData["ErrorMessage"] = result.Message;
            return Page();
        }
    }
}
