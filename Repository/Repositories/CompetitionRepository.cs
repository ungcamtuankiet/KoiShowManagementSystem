using Microsoft.EntityFrameworkCore;
using Repository.Entities;
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
        public async Task<IList<Competition>> GetAll()
        {
            return await _context.Competitions.Include(c => c.Category).ToListAsync();
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

        public async Task CreateCompetition(Competition competition)
        {
            await _context.Competitions.AddAsync(competition);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCompetition(Competition competition)
        {
            _context.Competitions.Update(competition);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCompetition(Competition competition)
        {
            _context.Competitions.Remove(competition);
            await _context.SaveChangesAsync();
        }
    }
}
