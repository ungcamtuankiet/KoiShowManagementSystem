using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.Dtos.Category;
using Repository.Dtos.Koi;
using Repository.Enum;
using Service.IService;
using Service.Service;

namespace KoiShowManagementSystem.Pages.Koi
{
    public class UpdateKoiModel : PageModel
    {
        private readonly IKoiService _koiService;
        private readonly IAuthService _authService;
        private readonly IFileService _fileService;

        public UpdateKoiModel(IKoiService koiService, IAuthService authService, IFileService fileService)
        {
            _koiService = koiService;
            _authService = authService;
            _fileService = fileService;
        }

        [BindProperty]
        public UpdateKoiDto UpdateKoiDto { get; set; }
        public string? UserRole { get; private set; }
        public int Id { get; set; }
        public IEnumerable<SelectListItem> koiVarieties { get; set; }
        public string fileName { get; set; }
        public IActionResult GetKoiAvatarImage(string fileName)
        {
            var avatarBytes = _fileService.GetKoiAvatar(fileName).Result;

            if (avatarBytes == null)
            {
                return NotFound(); 
            }

            return File(avatarBytes, "image/jpeg"); 
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var userRole = await _authService.GetUserRole(UserRole);
            var getKoi = await _koiService.GetKoiById(id);
            if (userRole != "Member")
            {
                await _authService.ClearSession();
                return RedirectToPage("/Index");
            }
            var koi = await _koiService.GetKoiById(id);
            UserRole = await _authService.GetUserRole("AdminRole");
            koiVarieties = Enum.GetValues(typeof(KoiVariety))
                               .Cast<KoiVariety>()
                               .Select(v => new SelectListItem
                               {
                                   Value = v.ToString(),
                                   Text = v.ToString()
                               })
                               .ToList();
            if (koi == null)
            {
                return NotFound();
            }
            UpdateKoiDto = new UpdateKoiDto()
            {
                Name = koi.Name,
                Variety = koi.Variety,
                Size = koi.Size,
                Description = koi.Description
            };

            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var response = await _koiService.UpdateKoi(UpdateKoiDto, id);

            if (response.Code == 0)
            {
                TempData["SuccessMessage"] = response.Message;
                return RedirectToPage("/Koi/MyKoiList");
            }
            var koi = await _koiService.GetKoiById(id);
            UserRole = await _authService.GetUserRole("AdminRole");
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
        public async Task<IActionResult> OnPostLogout()
        {
            await _authService.ClearSession();
            return RedirectToPage("/Index");
        }
    }
}
