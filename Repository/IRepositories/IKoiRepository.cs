using Repository.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepositories
{
    public interface IKoiRepository
    {
        Task<KoiFish?> GetKoiById(int koiId);
        Task<IEnumerable<KoiFish>> GetAllKoiForCompetition(int competitionId);
        Task AddKoiRegistration(KoiRegistration registration);
    }
}
