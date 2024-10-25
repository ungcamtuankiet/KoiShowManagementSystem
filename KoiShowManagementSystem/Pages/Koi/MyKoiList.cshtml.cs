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
        private readonly IAuthService _authService;

        public KoiListModel(IKoiService koiService, IFileService fileService, IAuthService authService)
        {
            _koiService = koiService;
            _fileService = fileService;
            _authService = authService;
        }

        public List<KoiFish> KoiFishList { get; set; }
        public UpdateKoiDto UpdateKoi { get; set; }
        public string fileName { get; set; }
        public int Koi_Id { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole == "Member")
            {
                if (userId == null)
                {
                    return RedirectToPage("/User/Login");
                }

                KoiFishList = await _koiService.GetKoiFishByUserIdAsync(userId.Value);
                return Page();
            }
            TempData["ErrorMessage"] = "You don't have permission to access this page";
            await _authService.ClearSession();
            return RedirectToPage("/Authen/Login");
        }

        public async Task<IActionResult> GetKoiAvatarImage(string fileName)
        {
            var avatarBytes = await _fileService.GetKoiAvatar(fileName);
            if (avatarBytes == null)
            {
                return NotFound();
            }

            return File(avatarBytes, "image/jpeg"); 
        }
        public async Task<IActionResult> OnPostLogout()
        {
            await _authService.ClearSession();
            return RedirectToPage("/Index");
        }
    }
}
