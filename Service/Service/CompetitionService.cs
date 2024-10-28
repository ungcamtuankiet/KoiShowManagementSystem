using Repository.Dtos.Response;
using Repository.Entities;
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
        private readonly IKoiRegistationRepository _koiRegistationRepository;

        public CompetitionService(ICompetitionRepository competitionRepository, IKoiRegistationRepository koiRegistationRepository)
        {
            _competitionRepository = competitionRepository;
            _koiRegistationRepository = koiRegistationRepository;
        }
        public async Task<IList<Competition>> GetCompetitionList()
        {
            return await _competitionRepository.GetAll();
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
            if (competition.Name == null || competition.Amount == null || competition.Description == null || competition.StartDate == null || competition.EndDate == null || competition.Location == null)
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
                if(competition.Amount <= 0)
                {
                    return new Response { Code = 1, Message = "Amount of User must be more than 0", Data = null };
                }
                competition.CreatedAt = DateTime.Now;
                competition.Status = StatusShowEnum.InProgess.ToString();
                await _competitionRepository.CreateCompetition(competition);
                return new Response { Code = 0, Message = "Create Competition Successfully", Data = competition };
            }
        }

        public async Task<Response> UpdateCompetition(Competition competition)
        {

            if (competition.Name == null || competition.Amount == null || competition.Description == null || competition.StartDate == null || competition.EndDate == null || competition.Location == null)
            {
                return new Response() { Code = 1, Message = "Please fill all information", Data = null };
            }
            else
            {
                if (competition.EndDate < competition.StartDate)
                {
                    return new Response() { Code = 1, Message = "End date can not before start date", Data = null };

                }
                if (competition.StartDate <= DateTime.Now)
                {
                    return new Response { Code = 1, Message = "Start date must be after today", Data = null };
                }
                if (competition.Amount <= 0)
                {
                    return new Response { Code = 1, Message = "Amount of User must be more than 0", Data = null };
                }
                competition.UpdatedAt = DateTime.Now;
                await _competitionRepository.UpdateCompetition(competition);
                return new Response { Code = 0, Message = "Update Competition Successfully", Data = competition };
            }
        }

        public async Task<Response> DeleteCompetition(Competition competition)
        {
            await _competitionRepository.DeleteCompetition(competition);
            return new Response() { Code = 0, Message = "Delete Competition Successfully", Data = null };
        }

        public async Task<Response> StartCompetition(int id)
        {
            var getCompetiton = await _competitionRepository.GetCompetitionById(id);
            var getKoiRegistation = await _koiRegistationRepository.GetKoiRegistationByCompetitionId(id);
            int countRegistation = getKoiRegistation.Count();
            if(getCompetiton.Amount > countRegistation)
            {
                return new Response() { Code = 1, Message = "The number of contest registrations is not enough so the contest cannot start yet.", Data = null };
            }
            getCompetiton.Status = StatusShowEnum.Starting.ToString();
            return new Response() { Code = 1, Message = "The number of contest registrations is not enough so the contest cannot start yet.", Data = null };
        }
    }
}
