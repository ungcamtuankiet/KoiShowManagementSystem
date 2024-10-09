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
        Task<IEnumerable<KoiFish>> GetListKoiFish();
        Task<KoiFish> GetKoiFishById(int id);
        Task<KoiFish> RegisterKoi(KoiFish fish);
        Task<KoiFish> UpdateKoi(KoiFish fish);
        Task<KoiFish> DeleteKoi(KoiFish fish);
    }
}
