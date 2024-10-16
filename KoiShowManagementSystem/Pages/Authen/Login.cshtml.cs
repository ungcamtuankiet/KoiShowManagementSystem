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
            TempData["SuccessMessage"] = user.Message;
            if(getUser.Role == "Member")
            {
                return RedirectToPage("/User/UserPage");
            }
            else if(getUser.Role == "Staff")
            {
                return RedirectToPage("/Staff/StaffPage");
            }
            else if(getUser.Role == "Referee")
            {
                return RedirectToPage("/Referee/RefereePage");
            }
            else
            {
                return RedirectToPage("/Admin/AdminPage");
            }
        }
    }
}
