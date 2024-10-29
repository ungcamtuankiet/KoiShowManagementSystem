using Repository.Entities;

namespace Repository.IRepositories
{
    public interface ICompetitionRepository
    {
        Task<IList<Competition>> GetAll();
        Task<IEnumerable<Competition>> GetCompetitionsByStatus(string status);
        Task<Competition?> GetCompetitionById(int id);
        Task CreateCompetition(Competition competition);
        Task UpdateCompetition(Competition competition);
        Task DeleteCompetition(Competition competition);
    }
}


