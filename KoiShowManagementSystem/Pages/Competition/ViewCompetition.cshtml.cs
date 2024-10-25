using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Entites;

namespace KoiShowManagementSystem.Pages.Competition
{
    public class ViewCompetitionModel : PageModel
    {
        private readonly Repository.KoiShowManagementSystemContext _context;

        public ViewCompetitionModel(Repository.KoiShowManagementSystemContext context)
        {
            _context = context;
        }

        public IList<Repository.Entites.Competition> Competition { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Competition = await _context.Competitions
                .Include(c => c.Category).ToListAsync();
        }
    }
}
