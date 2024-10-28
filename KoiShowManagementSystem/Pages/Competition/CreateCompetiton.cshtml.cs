using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service.IService;

namespace KoiShowManagementSystem.Pages.Competition
{
    public class CreateCompetitonModel : PageModel
    {
        private readonly IAuthService _authService;
        private readonly ICompetitionService _competitionService;
        private readonly ICategoryService _categoryService;
        public CreateCompetitonModel(IAuthService authService, ICompetitionService competitionService, ICategoryService categoryService)
        {
            _authService = authService;
            _competitionService = competitionService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> OnGet()
        {
            UserRole = await _authService.GetUserRole("StaffRole");
            ViewData["CategoryId"] = new SelectList(await _categoryService.GetCategoriesAsync(), "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Repository.Entities.Competition Competition { get; set; } = default!;
        public string? UserRole { get; private set; }

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _competitionService.CreateCompetition(Competition);
            if(result.Code == 1)
            {
                TempData["ErrorMessage"] = result.Message;
                return Page();
            }
            TempData["SuccessMessage"] = result.Message;
            return RedirectToPage("/Competition/CompetitionsModel");
        }
    }
}
