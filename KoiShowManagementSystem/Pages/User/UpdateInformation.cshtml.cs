using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.Entities;
using Service.IService;
using Service.Service;

namespace KoiShowManagementSystem.Pages.User
{
    public class UpdateInformationModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public UpdateInformationModel(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }
        [BindProperty]
        public Repository.Entities.User User { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole == "Member")
            {
                if (id == null)
                {
                    return NotFound();
                }

                var user = await _userService.GetUserById(id);
                if (user == null)
                {
                    return NotFound();
                }
                User = user;
                return Page();
            }
            TempData["ErrorMessage"] = "You don't have permission to access this page";
            await _authService.ClearSession();
            return RedirectToPage("/Authen/Login");
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var existingUser = await _userService.GetUserById(User.Id);
            existingUser.FullName = User.FullName;
            existingUser.Email = User.Email;
            existingUser.PhoneNumber = User.PhoneNumber;
            existingUser.Address = User.Address;
            var result = await _userService.UpdateUser(existingUser);
            if (result.Code == 0)
            {
                TempData["SuccessMessage"] = result.Message;
                return Page();
            }
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
