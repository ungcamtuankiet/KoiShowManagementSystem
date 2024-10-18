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

        
    }
}
