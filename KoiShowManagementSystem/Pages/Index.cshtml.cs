using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace KoiShowManagementSystem.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public string CompetitionInfo { get; set; }
        public List<string> Prizes { get; set; } = new List<string>();
        public string Rules { get; set; }
        public string ScoringCriteria { get; set; }
        public List<string> News { get; set; } = new List<string>();

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            CompetitionInfo = "Chào mừng bạn đến với cuộc thi cá Koi! Đây là nơi để các nghệ nhân và người yêu thích cá Koi thể hiện tài năng và sự sáng tạo của mình. Cuộc thi không chỉ là một sự kiện để tranh tài mà còn là cơ hội để giao lưu và học hỏi giữa những người đam mê cá Koi.";

            Prizes = GetPrizes();

            Rules =
                    "<ul>" +
                    "<li>Cá Koi tham gia phải đạt tiêu chuẩn về sức khỏe và hình dáng.</li>" +
                    "<li>Mỗi thí sinh chỉ được đăng ký tối đa 3 con cá.</li>" +
                    "<li>Các con cá sẽ được phân loại theo kích thước và giống loài.</li>" +
                    "<li>Ban giám khảo sẽ có quyền loại bỏ những con cá không đạt yêu cầu.</li>" +
                    "</ul>";

            ScoringCriteria = 
                              "<ul>" +
                              "<li><strong>Màu sắc</strong>: Màu sắc phải sắc nét, tươi sáng và đều khắp trên thân cá.</li>" +
                              "<li><strong>Hình dáng</strong>: Cá phải có hình dáng đối xứng, cân đối, và không có các vết hư tổn.</li>" +
                              "<li><strong>Da và vảy</strong>: Da của cá phải sáng bóng, sạch sẽ và mịn màng.</li>" +
                              "<li><strong>Tư thế bơi</strong>: Cá koi cần có chuyển động uyển chuyển, mượt mà, và tư thế bơi tự nhiên.</li>" +
                              "</ul>";

            News = GetNews();
        }

        private List<string> GetPrizes()
        {
            return new List<string>
            {
                "Giải nhất: 10 triệu đồng",
                "Giải nhì: 5 triệu đồng",
                "Giải ba: 2 triệu đồng"
            };
        }

        private List<string> GetNews()
        {
            return new List<string>
            {
                "Tin tức 1: Cuộc thi sẽ diễn ra vào tháng 1.",
                "Tin tức 2: Đăng ký tham gia ngay hôm nay!"
            };
        }
    }
}
