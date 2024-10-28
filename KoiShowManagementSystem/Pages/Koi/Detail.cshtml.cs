using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Entities;
using Service.IService;

namespace KoiShowManagementSystem.Pages.Koi
{
    public class DetailModel : PageModel
    {
        private readonly IKoiService _koiService;
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public DetailModel(IKoiService koiService, IAuthService authService, IUserService userService)
        {
            _koiService = koiService;
            _authService = authService;
            _userService = userService;
        }

        public KoiFish KoiFish { get; set; } = default!;
        public string FullNameUser { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var koifish = await _koiService.GetKoiById((int)id);
            var getUserId = koifish.UserId;
            var getUser = await _userService.GetUserById(getUserId);
            FullNameUser = getUser.FullName;
            if (koifish == null)
            {
                return NotFound();
            }
            else
            {
                KoiFish = koifish;
            }
            return Page();
        }
        public async Task<IActionResult> OnPostLogout()
        {
            await _authService.ClearSession();
            return RedirectToPage("/Index");
        }
    }
}
