using Repository.Dtos;
using Repository.Dtos.Competition;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface ICompetitionService
    {
        Task<ResponseDto> RegisterCompetitionAsync(RegisterCompetitionDTO competitionDTO);
    }
}