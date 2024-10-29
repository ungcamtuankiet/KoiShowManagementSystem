using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository;
using Repository.Entities;
using Repository.IRepositories;
using Service.IService;
using Service.Service;

namespace KoiShowManagementSystem.Pages.Staff.Competition
{
    public class CreateModel : PageModel
    {
        private readonly ICompetitionService _competitionService;
        private readonly ICategoryService _categoryService;
        private readonly IAuthService _authService;

        public CreateModel(ICategoryService categoryService, ICompetitionService competitionService, IAuthService authService)
        {
            _categoryService = categoryService;
            _competitionService = competitionService;
            _authService = authService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole == "Staff")
            {
                ViewData["CategoryId"] = new SelectList(await _categoryService.GetCategoriesAsync(), "Id", "Name");
                return Page();
            }
            TempData["ErrorMessage"] = "You don't have permission to access this page";
            await _authService.ClearSession();
            return RedirectToPage("/Authen/Login");
        }

        [BindProperty]
        public Repository.Entities.Competition Competition { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }
                var result = await _competitionService.CreateCompetition(Competition);
                if (result.Code == 0)
                {
                    TempData["SuccessMessage"] = result.Message;
                    return RedirectToPage("/Staff/Competition/Index");
                }
                ViewData["CategoryId"] = new SelectList(await _categoryService.GetCategoriesAsync(), "Id", "Name");
                TempData["ErrorMessage"] = result.Message;
                return Page();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex;
                return Page();
            }
        }
        public async Task<IActionResult> OnPostLogout()
        {
            await _authService.ClearSession();
            return RedirectToPage("/Index");
        }
    }
}
