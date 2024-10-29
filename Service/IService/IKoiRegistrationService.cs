using Repository.Dtos.Response;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IKoiRegistrationService
    {
        Task<IList<KoiRegistration>> GetListKoiRegistrationAsync();
        Task<Response> AcceptKoiRegistration(int id, int competitionId);
        Task<Response> RejectKoiRegistration(int id, string reason);

    }
}
