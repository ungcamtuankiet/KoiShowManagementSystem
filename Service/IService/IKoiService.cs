using Repository.Dtos.Koi;
using Repository.Dtos.Response;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IKoiService
    {
        Task<KoiFish> GetKoiById(int id);
        Task<IList<KoiFish>> GetAllKoiFish();
        Task<List<KoiFish>> GetAllKoiFishForStaff();
        Task<Response> RegisterKoi(RegisterKoi registerKoiDto, int? userId);
        Task<IEnumerable<KoiFish>> GetKoiForCompetition(int competitionId);
        Task<List<KoiFish>> GetKoiFishByUserIdAsync(int userId);
        Task<Response> DeleteKoi(int id);
        Task<Response> UpdateKoi(UpdateKoiDto koiFish, int id);
        Task<Response> ApprovalKoi(int koiId);
        Task<Response> Reject(int koiId, string reason);
    }
}
