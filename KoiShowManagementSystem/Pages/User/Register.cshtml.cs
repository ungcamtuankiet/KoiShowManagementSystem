using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Dtos.User;
using Service.IService;

namespace KoiShowManagementSystem.Pages.User
{
    public class RegisterModel : PageModel
    {
        private readonly IUserService _userService;

        public RegisterModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public RegisterUserDto Input { get; set; } = new RegisterUserDto();

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterUserAsync(Input);
                if (result.Code == 0)
                {
                    return RedirectToPage("/User/Login");
                }
                ModelState.AddModelError(string.Empty, "User with this email already exists.");
            }
            return Page();
        }
    }
}
