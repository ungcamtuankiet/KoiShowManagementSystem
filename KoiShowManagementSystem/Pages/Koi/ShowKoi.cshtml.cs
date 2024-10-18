using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Repository.Entites;
using Repository.IRepositories;

namespace KoiShowManagementSystem.Pages.Koi
{
    public class ShowKoiModel : PageModel
    {
        private readonly IKoiRepository _koiRepository;

        public KoiFish KoiFish { get; set; } // Sử dụng lớp KoiFish

        public ShowKoiModel(IKoiRepository koiRepository)
        {
            _koiRepository = koiRepository;
        }

        public async Task<IActionResult> OnGetAsync(int koiId)
        {
            // Lấy thông tin cá koi từ cơ sở dữ liệu
            KoiFish = await _koiRepository.GetKoiById(koiId);

            if (KoiFish == null)
            {
                return NotFound(); // Trả về lỗi nếu không tìm thấy cá koi
            }

            return Page();
        }
    }
}
