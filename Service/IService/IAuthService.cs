using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IAuthService
    {
        Task<string> GetUserRole(string userRole);
        Task ClearSession();
    }
}
