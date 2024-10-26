using Repository.Entites;
using Repository.IRepositories;
using System.Threading.Tasks;

namespace KoiShowManagementSystem.Repository
{
    public class CompetitionRepository : ICompetitionRepository
    {
        // Implement your database context here

        public async Task AddCompetitionAsync(Competition competition)
        {
            // Implement the logic to add a competition to the database
            throw new NotImplementedException();
        }

        public Task<Competition?> GetCompetitionById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Competition>> GetCompetitionsByStatus(string status)
        {
            throw new NotImplementedException();
        }
    }
}