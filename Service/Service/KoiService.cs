
using Repository.Dtos.Koi;
using Repository.Dtos.Response;
using Repository.Entites;
using Repository.IRepositories;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class KoiService : IKoiService
    {
        private readonly IKoiRepository _koiRepository;

        public KoiService(IKoiRepository repository)
        {
            _koiRepository = repository;
        }

        public async Task RegisterKoi(int koiId, int competitionId)
        {
            var registration = new KoiRegistration
            {
                KoiId = koiId,
                CompetitionId = competitionId,
                RegistrationDate = DateTime.Now,
                Status = "Pending"
            };

            await _koiRepository.AddKoiRegistration(registration);
        }

        public async Task<IEnumerable<KoiFish>> GetKoiForCompetition(int competitionId)
        {
            return await _koiRepository.GetAllKoiForCompetition(competitionId);
        }
    }
}
