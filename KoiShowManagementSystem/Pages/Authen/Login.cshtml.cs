using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Dtos.User;
using Service.IService;

namespace KoiShowManagementSystem.Pages.Authen
{
    public class LoginModel : PageModel
    {
        private readonly IUserService _userService;

        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public LoginUserDto Input { get; set; } = new LoginUserDto();

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userService.Login(Input);
            var getUser = await _userService.GetUserByEmail(Input.Email);
            if (user.Code == 1)
            {
                TempData["ErrorMessage"] = user.Message;
                return Page();
            }

            // Store user session data
            HttpContext.Session.SetInt32("UserId", getUser.Id);
            HttpContext.Session.SetString("UserRole", getUser.Role.ToString());
            HttpContext.Session.SetString("UserName" , getUser.FullName);
            TempData["SuccessMessage"] = user.Message;
            var getUserRole = HttpContext.Session.GetString("UserRole");
            if (getUserRole == "Staff")
                return RedirectToPage("/Staff/Competition/Index");
            if (getUserRole == "Admin")
                return RedirectToPage("/Admin/Account/Index");
            return RedirectToPage("/Index");
        }
    }
}
