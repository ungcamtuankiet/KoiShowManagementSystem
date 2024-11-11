using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Entities;
using Service.IService;

namespace KoiShowManagementSystem.Pages.Staff.ManageKoi
{
    public class IndexModel : PageModel
    {
        private readonly IKoiService _koiService;
        private readonly IFileService _fileService;
        private readonly IAuthService _authService;

        public IndexModel(IKoiService koiService, IAuthService authService, IFileService fileService)
        {
            _koiService = koiService;
            _authService = authService;
            _fileService = fileService;
        }

        public IList<KoiFish> Kois { get; set; } 
        public string fileName { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole == "Staff")
            {
                Kois = await _koiService.GetAllKoiFishForStaff();
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
