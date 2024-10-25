using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Dtos.Koi;
using Repository.Entites;
using Service.IService;
using Service.Service;

namespace KoiShowManagementSystem.Pages.Koi
{
    public class KoiListModel : PageModel
    {
        private readonly IKoiService _koiService;
        private readonly IFileService _fileService;

        public KoiListModel(IKoiService koiService, IFileService fileService)
        {
            _koiService = koiService;
            _fileService = fileService;
        }

        public List<KoiFish> KoiFishList { get; set; }
        public UpdateKoiDto UpdateKoi { get; set; }
        public string fileName { get; set; }
        public int Koi_Id { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToPage("/User/Login");
            }

            // L?y danh sách Koi theo UserId
            KoiFishList = await _koiService.GetKoiFishByUserIdAsync(userId.Value);

            if (KoiFishList == null || !KoiFishList.Any())
            {
                TempData["ErrorMessage"] = "No Koi found for this user.";
            }

            return Page();
        }

        public async Task<IActionResult> GetKoiAvatarImage(string fileName)
        {
            var avatarBytes = await _fileService.GetKoiAvatar(fileName);

            if (avatarBytes == null)
            {
                return NotFound(); // Tr? v? 404 n?u không tìm th?y ?nh
            }

            // Tr? v? file ?nh
            return File(avatarBytes, "image/jpeg"); // Ho?c "image/png" tùy lo?i ?nh
        }


        public async Task<IActionResult> OnPostDeleteKoiAsync(int KoiId)
        {
            // Kiểm tra xem KoiId có giá trị không
            if (KoiId == 0)
            {
                TempData["ErrorMessage"] = "Koi ID is invalid.";
                return RedirectToPage("/Koi/MyKoiList");
            }

            // Tìm Koi dựa trên ID
            var koi = await _koiService.GetKoiById(KoiId);

            // Kiểm tra nếu koi bị null
            if (koi == null)
            {
                TempData["ErrorMessage"] = "Koi does not exist.";
                return RedirectToPage("/Koi/MyKoiList");
            }

            // Tiếp tục xử lý xóa nếu Koi tồn tại
            var result = await _koiService.DeleteKoi(KoiId);
            if (result.Code == 0)
            {
                TempData["SuccessMessage"] = result.Message;
                return RedirectToPage("/Koi/MyKoiList");
            }
            else
            {
                TempData["ErrorMessage"] = result.Message;
            }
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
