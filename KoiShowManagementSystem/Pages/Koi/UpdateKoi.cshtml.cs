using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.Dtos.Koi;
using Repository.Enum;
using Service.IService;

namespace KoiShowManagementSystem.Pages.Koi
{
    public class UpdateKoiModel : PageModel
    {
        private readonly IKoiService _koiService;

        public UpdateKoiModel(IKoiService koiService)
        {
            _koiService = koiService;
        }

        [BindProperty]
        public UpdateKoiDto UpdateKoiDto { get; set; } = new UpdateKoiDto();

        public IEnumerable<SelectListItem> KoiVarieties { get; set; }

        public void OnGetAsync(int id)
        {
            // L?y danh sách các loài Koi t? enum
            KoiVarieties = Enum.GetValues(typeof(KoiVariety))
                                .Cast<KoiVariety>()
                                .Select(v => new SelectListItem
                                {
                                    Value = v.ToString(),
                                    Text = v.ToString()
                                })
                                .ToList();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // G?i service ?? c?p nh?t Koi
            var response = await _koiService.UpdateKoi(UpdateKoiDto, id);
            if (response.Code != 0)
            {
                TempData["ErrorMessage"] = response.Message;
                return Page();
            }

            TempData["SuccessMessage"] = response.Message;
            return RedirectToPage("/User/UserPage"); // Redirect sau khi c?p nh?t thành công
        }
    }
}
