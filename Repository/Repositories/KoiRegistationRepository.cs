using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Entities;
using Repository.Enum;
using Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class KoiRegistationRepository : IKoiRegistationRepository
    {
        private readonly KoiShowManagementSystemContext _context;

        public KoiRegistationRepository(KoiShowManagementSystemContext context)
        {
            _context = context;
        }
        public async Task<IList<KoiRegistration>> GetKoiRegistationByCompetitionId(int competitionId)
        {
            return await _context.KoiRegistrations.Where(kr => kr.CompetitionId == competitionId && kr.Status == KoiRegistrationCompetitionEnum.Accepted.ToString()).ToListAsync();
        }
        public async Task<IList<KoiRegistration>> GetListKoiPending(int competitionId)
        {
            return await _context.KoiRegistrations.Where(kr => kr.Status == KoiRegistrationCompetitionEnum.Pending.ToString()).ToListAsync();
        }

        public async Task<KoiRegistration> GetKoiRegistrationById(int id)
        {
            return await _context.KoiRegistrations.FirstOrDefaultAsync(k => k.Id == id);
        }

        public async Task<IList<KoiRegistration>> ViewKoiRegistration()
        {
            return await _context.KoiRegistrations
                .Include(k => k.Competition)
                .Include(k => k.Koi)
                .Include(k => k.User)
                .ToListAsync();
        }
        public async Task RegisterKoiRegistration(KoiRegistration registration)
        {
            await _context.KoiRegistrations.AddAsync(registration);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateKoiRegistration(KoiRegistration registration)
        {
            _context.KoiRegistrations.Update(registration);
            await _context.SaveChangesAsync();
        }
    }
}
