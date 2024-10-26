using Repository.Dtos;
using Repository.Dtos.Competition;
using Repository.Entites;
using Repository.IRepositories;
using Service.IService;

namespace Service.Service
{
    public class CompetitionService : ICompetitionService
    {
        private readonly ICompetitionRepository _competitionRepository;

        public CompetitionService(ICompetitionRepository competitionRepository)
        {
            _competitionRepository = competitionRepository;
        }


        public async Task<ResponseDto> RegisterCompetitionAsync(RegisterCompetitionDTO competitionDTO)
        {
            try
            {
                var competition = new Competition
                {
                    Name = competitionDTO.Name,
                    Location = competitionDTO.Location,
                    StartDate = competitionDTO.StartDate,
                    EndDate = competitionDTO.EndDate,
                    Description = competitionDTO.Description,
                    Status = "Pending" // You might want to set an initial status
                };

                //await _competitionRepository.AddCompetitionAsync(competition);
                return new ResponseDto { Code = 0, Message = "Competition registered successfully" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Code = 1, Message = $"Error registering competition: {ex.Message}" };
            }
        }
    }
}