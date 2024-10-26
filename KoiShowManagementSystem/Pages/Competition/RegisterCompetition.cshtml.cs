using KoiShowManagementSystem.Pages.Koi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Threading.Tasks;

public class RegisterCompetitionModel : PageModel
{
    [BindProperty]
    public IFormFile UploadedImage { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (UploadedImage != null)
        {
            var filePath = Path.Combine("wwwroot/images", UploadedImage.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await UploadedImage.CopyToAsync(stream);
            }
        }

        return RedirectToPage("/Index"); 
                                               
    }
}
