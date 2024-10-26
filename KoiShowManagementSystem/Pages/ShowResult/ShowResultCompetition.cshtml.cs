using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Entites;

public class ShowResultCompetitionModel : PageModel
{
    private readonly KoiShowManagementSystemContext _context;

    public ShowResultCompetitionModel(KoiShowManagementSystemContext context)
    {
        _context = context;
    }

    public List<Result> Result { get; set; }
    //public List<PredictionResult> PredictionResults { get; set; }
    //public List<KoiAchievement> KoiAchievements { get; set; }

    public async Task OnGetAsync()
    {
        var listResult = new List<Result>();
        listResult = await _context.Results.ToListAsync();
        Result = listResult;
        //PredictionResults = await _context.PredictionResults.ToListAsync();
        //KoiAchievements = await _context.KoiAchievements.ToListAsync();
    }
}
