using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.IService;

namespace KoiShowManagementSystem.Pages.Competition
{
    public class CompetitionsModelModel : PageModel
    {
        private readonly ICompetitionService _competitionService;

        public CompetitionsModelModel(ICompetitionService competitionService)
        {
            _competitionService = competitionService;
        }
        public IEnumerable<Repository.Entites.Competition> Competitions { get; set; }

        public async Task OnGetAsync(string status)
        {
            Competitions = await _competitionService.GetCompetitions(status);
        }
    }
}
