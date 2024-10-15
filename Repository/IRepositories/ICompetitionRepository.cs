using Repository.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepositories
{
    public interface ICompetitionRepository
    {
        Task<IEnumerable<Competition>> GetCompetitionsByStatus(string status);
        Task<Competition?> GetCompetitionById(int id);
    }
}
