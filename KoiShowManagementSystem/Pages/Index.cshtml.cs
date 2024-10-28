using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Repository.Entities;
using Service.IService;
using System.Collections.Generic;

namespace KoiShowManagementSystem.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger; 
        private readonly IAuthService _authService;
        private readonly IKoiService _koiService;

        public IList<KoiFish> KoiFishList { get; set; }
        public string CompetitionInfo { get; set; }
        public List<string> Prizes { get; set; } = new List<string>();
        public string Rules { get; set; }
        public string ScoringCriteria { get; set; }
        public List<string> News { get; set; } = new List<string>();

        public IndexModel(ILogger<IndexModel> logger, IAuthService authService, IKoiService koiService)
        {
            _logger = logger;
            _authService = authService;
            _koiService = koiService;
        }

        public async Task OnGetAsync()
        {
            CompetitionInfo = "Chào m?ng b?n ??n v?i cu?c thi cá Koi! ?ây là n?i ?? các ngh? nhân và ng??i yêu thích cá Koi th? hi?n tài n?ng và s? sáng t?o c?a mình. Cu?c thi không ch? là m?t s? ki?n ?? tranh tài mà còn là c? h?i ?? giao l?u và h?c h?i gi?a nh?ng ng??i ?am mê cá Koi.";
            KoiFishList = await _koiService.GetAllKoiFish();
            Prizes = GetPrizes();

            Rules =
                    "<ul>" +
                    "<li>Cá Koi tham gia ph?i ??t tiêu chu?n v? s?c kh?e và hình dáng.</li>" +
                    "<li>M?i thí sinh ch? ???c ??ng ký t?i ?a 3 con cá.</li>" +
                    "<li>Các con cá s? ???c phân lo?i theo kích th??c và gi?ng loài.</li>" +
                    "<li>Ban giám kh?o s? có quy?n lo?i b? nh?ng con cá không ??t yêu c?u.</li>" +
                    "</ul>";

            ScoringCriteria =
                              "<ul>" +
                              "<li><strong>Màu s?c</strong>: Màu s?c ph?i s?c nét, t??i sáng và ??u kh?p trên thân cá.</li>" +
                              "<li><strong>Hình dáng</strong>: Cá ph?i có hình dáng ??i x?ng, cân ??i, và không có các v?t h? t?n.</li>" +
                              "<li><strong>Da và v?y</strong>: Da c?a cá ph?i sáng bóng, s?ch s? và m?n màng.</li>" +
                              "<li><strong>T? th? b?i</strong>: Cá koi c?n có chuy?n ??ng uy?n chuy?n, m??t mà, và t? th? b?i t? nhiên.</li>" +
                              "</ul>";

            News = GetNews();
        }

        private List<string> GetPrizes()
        {
            return new List<string>
            {
                "Gi?i nh?t: 10 tri?u ??ng",
                "Gi?i nhì: 5 tri?u ??ng",
                "Gi?i ba: 2 tri?u ??ng"
            };
        }

        private List<string> GetNews()
        {
            return new List<string>
            {
                "Tin t?c 1: Cu?c thi s? di?n ra vào tháng 1.",
                "Tin t?c 2: ??ng ký tham gia ngay hôm nay!"
            };
        }
        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Index");
        }
    }
}