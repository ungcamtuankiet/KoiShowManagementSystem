using Repository.Dtos.Koi;
using Repository.Dtos.Response;
using Repository.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IKoiService
    {
        Task RegisterKoi(int koiId, int competitionId);
        Task<IEnumerable<KoiFish>> GetKoiForCompetition(int competitionId);
    }
}
