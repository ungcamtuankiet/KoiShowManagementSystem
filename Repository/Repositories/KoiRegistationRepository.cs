using Microsoft.EntityFrameworkCore;
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
            return await _context.KoiRegistrations.Where(kr => kr.CompetitionId == competitionId && kr.Status == KoiRegistationEnum.Approval.ToString()).ToListAsync();
        }
    }
}
