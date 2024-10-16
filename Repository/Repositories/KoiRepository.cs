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
        public async Task<IEnumerable<KoiFish>> GetAllKoiFish()
        {
            return await _context.KoiFishes.Where(k => k.Status == "Active").ToListAsync();
        }
        public async Task<List<KoiFish>> GetKoiFishByUserIdAsync(int userId)
        {
            return await _context.KoiFishes
                                 .Where(k => k.UserId == userId)
                                 .ToListAsync();
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

        public async Task AddKoiRegistration(KoiFish koiFish)
        {
            await _context.KoiFishes.AddAsync(koiFish);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteKoi(int id)
        {
            var koiFish = await _context.KoiFishes.FirstOrDefaultAsync(k => k.Id == id);
            _context.KoiFishes.Remove(koiFish);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateKoi(KoiFish koiFish)
        {
            _context.KoiFishes.Update(koiFish);
            await _context.SaveChangesAsync();
        }
    }
}
