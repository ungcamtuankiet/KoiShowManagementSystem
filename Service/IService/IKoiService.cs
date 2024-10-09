using Repository.Dtos.Koi;
using Repository.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IKoiService
    {
        Task<Response> GetListKoiFish();
        Task<Response> AddNewKoi(RegisterKoi registerKoi);
    }
}
