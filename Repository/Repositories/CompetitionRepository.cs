using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Entites;
using Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class CompetitionRepository : ICompetitionRepository
    {
        private readonly KoiShowManagementSystemContext _context;

        public CompetitionRepository(KoiShowManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Competition>> GetCompetitionsByStatus(string status)
        {
            return await _context.Competitions
                .Where(c => c.Status == status)
                .ToListAsync();
        }

        public async Task<Competition?> GetCompetitionById(int id)
        {
            return await _context.Competitions.FindAsync(id);
        }
    }
}
