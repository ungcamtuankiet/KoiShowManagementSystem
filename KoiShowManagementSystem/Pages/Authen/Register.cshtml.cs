using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Dtos.User;
using Service.IService;

namespace KoiShowManagementSystem.Pages.Authen
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
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var result = await _userService.Register(Input);
                if (result.Code == 1)
                {
                    foreach (var error in result.Message)
                    {
                        TempData["ErrorMessage"] = result.Message;
                    }
                    TempData["ErrorMessage"] = result.Message;
                    return Page();
                }
                SuccessMessage = result.Message;
                return RedirectToPage("/Authen/Login");
            }
            catch (Exception ex)
            {
                TempData[SuccessMessage] = ex.Message;
                return Page();
            }
        }
    }
}
