using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service.IService;

namespace KoiShowManagementSystem.Pages.Staff.Competition
{
    public class EditModel : PageModel
    {
        private readonly ICompetitionService _competitionService;
        private readonly IAuthService _authService;
        private readonly ICategoryService _categoryService;

        public EditModel(ICompetitionService competitionService, ICategoryService categoryService, IAuthService authService)
        {
            _competitionService = competitionService;
            _categoryService = categoryService;
            _authService = authService;
        }

        [BindProperty]
        public Repository.Entities.Competition Competition { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole == "Staff")
            {
                if (id == null)
                {
                    return NotFound();
                }

                var competition = await _competitionService.GetCompetitionById((int)id);
                if (competition == null)
                {
                    return NotFound();
                }
                Competition = competition;
                ViewData["CategoryId"] = new SelectList(await _categoryService.GetCategoriesAsync(), "Id", "Name");
                return Page();
            }
            TempData["ErrorMessage"] = "You don't have permission to access this page";
            await _authService.ClearSession();
            return RedirectToPage("/Authen/Login");  
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _competitionService.UpdateCompetition(Competition);
            if (result.Code == 0)
            {
                TempData["SuccessMessage"] = result.Message;
                return RedirectToPage("/Staff/Competition/Index");
            }
            ViewData["CategoryId"] = new SelectList(await _categoryService.GetCategoriesAsync(), "Id", "Name");
            TempData["ErrorMessage"] = result.Message;
            return Page();
        }
        public async Task<IActionResult> OnPostLogout()
        {
            await _authService.ClearSession();
            return RedirectToPage("/Index");
        }
    }
}
