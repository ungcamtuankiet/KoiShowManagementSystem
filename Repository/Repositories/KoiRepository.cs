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
    public class KoiRepository : IKoiRepository
    {
        private readonly KoiShowManagementSystemContext _context;

        public KoiRepository(KoiShowManagementSystemContext context)
        {
            _context = context;
        }
        public async Task<KoiFish?> GetKoiById(int koiId)
        {
            return await _context.KoiFishes.FindAsync(koiId);
        }

        public async Task<IEnumerable<KoiFish>> GetAllKoiForCompetition(int competitionId)
        {
            return await _context.KoiRegistrations
                .Where(kr => kr.CompetitionId == competitionId && kr.Status == "Approved")
                .Select(kr => kr.Koi)
                .ToListAsync();
        }

        public async Task AddKoiRegistration(KoiRegistration registration)
        {
            _context.KoiRegistrations.Add(registration);
            await _context.SaveChangesAsync();
        }
    }
}
