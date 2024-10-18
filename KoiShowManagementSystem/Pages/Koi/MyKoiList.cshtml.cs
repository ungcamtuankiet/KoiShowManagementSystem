using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Dtos.Koi;
using Repository.Entites;
using Service.IService;

namespace KoiShowManagementSystem.Pages.Koi
{
    public class KoiListModel : PageModel
    {
        private readonly IKoiService _koiService;

        public KoiListModel(IKoiService koiService)
        {
            _koiService = koiService;
        }

        public List<KoiFish> KoiFishList { get; set; }
        public UpdateKoi UpdateKoi { get; set; }
        public int Koi_Id { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToPage("/User/Login");
            }

            KoiFishList = await _koiService.GetKoiFishByUserIdAsync(userId.Value);

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteKoiAsync(int deleteKoi)
        {
            var getKoi = await _koiService.GetKoiById(deleteKoi);
            var userId = HttpContext.Session.GetInt32("UserId");
            if (getKoi == null)
            {
                TempData["ErrorMessage"] = "Koi not exist";
            }
            var result = await _koiService.DeleteKoi(deleteKoi);
            if(result.Code == 0)
            {
                TempData["SuccessMessage"] = result.Message;
                KoiFishList = await _koiService.GetKoiFishByUserIdAsync(userId.Value);
                return Page();
            }
            TempData["ErrorMessage"] = result.Message;
            return Page();
        }

        public async Task<IActionResult> OnPostUpdateKoiAsync(int KoiId)
        {
            var result = await _koiService.UpdateKoi(UpdateKoi, KoiId);
            Koi_Id = KoiId;
            var userId = HttpContext.Session.GetInt32("UserId");
            if (result.Code == 0)
            {
                TempData["SuccessMessage"] = result.Message;
                KoiFishList = await _koiService.GetKoiFishByUserIdAsync(userId.Value);
                return Page();
            }
            TempData["ErrorMessage"] = result.Message;
            return Page();
        }
    }
}
