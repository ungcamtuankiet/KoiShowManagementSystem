using Repository.Dtos.Response;
using Repository.Entites;
using Repository.Enum;
using Repository.IRepositories;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class CompetitionService : ICompetitionService
    {
        private readonly ICompetitionRepository _competitionRepository;

        public CompetitionService(ICompetitionRepository competitionRepository)
        {
            _competitionRepository = competitionRepository;
        }

        public async Task<Competition> GetCompetitionById(int id)
        {
            return await _competitionRepository.GetCompetitionById(id);
        }

        public async Task<IEnumerable<Competition>> GetCompetitions(string status)
        {
            return await _competitionRepository.GetCompetitionsByStatus(status);
        }
        public async Task<Response> CreateCompetition(Competition competition)
        {
            if (competition.Name == null || competition.Description == null || competition.StartDate == null || competition.EndDate == null || competition.Location == null)
            {
                return new Response() { Code = 1, Message = "Please fill all information", Data = null };
            }
            else
            {
                if(competition.EndDate < competition.StartDate)
                {
                    return new Response() { Code = 1, Message = "End date can not before start date", Data = null };

                }
                if(competition.StartDate <= DateTime.Now)
                {
                    return new Response { Code = 1, Message =  "Start date must be after today",Data = null };
                }
                competition.CreatedAt = DateTime.Now;
                competition.Status = StatusEnum.Active.ToString();
                await _competitionRepository.CreateCompetition(competition);
                return new Response { Code = 0, Message = "Create Competition Successfully", Data = competition };
            }
        }
    }
}
