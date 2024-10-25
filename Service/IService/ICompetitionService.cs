using Repository.Dtos.Response;
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
        Task<Competition> GetCompetitionById(int id);
        Task<Response> CreateCompetition(Competition competition);
    }
}
