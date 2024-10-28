using Repository.Dtos.Response;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface ICompetitionService
    {
        Task<IList<Competition>> GetCompetitionList();
        Task<IEnumerable<Competition>> GetCompetitions(string status);
        Task<Competition> GetCompetitionById(int id);
        Task<Response> CreateCompetition(Competition competition);
        Task<Response> UpdateCompetition(Competition competition);
        Task<Response> DeleteCompetition(Competition competition);
        Task<Response> StartCompetition(int id);
    }
}
