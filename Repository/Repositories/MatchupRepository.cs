using Repository.Data;
using Repository.Entities;
using Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class MatchupRepository : IMatchupRepository
    {
        private readonly KoiShowManagementSystemContext _context;

        public MatchupRepository(KoiShowManagementSystemContext context)
        {
            _context = context;
        }

        public async Task AddMatChup(IEnumerable<Matchup> matchups)
        {
            await _context.Matchups.AddRangeAsync(matchups);
            await _context.SaveChangesAsync();
        }
    }
}
