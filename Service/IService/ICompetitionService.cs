using Repository.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface ICompetitionService
    {
        Task<IEnumerable<Competition>> GetCompetitions(string status);
    }
}
