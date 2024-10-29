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
    public class KoiRegistrationService : IKoiRegistrationService
    {
        private readonly IKoiRegistationRepository _repository;
        private readonly ICompetitionRepository _competitionRepository;
        private readonly IEmailService _emailService;
        private readonly ICompetitionService _competitionService;
        private readonly IUserService _userService;

        public KoiRegistrationService(IKoiRegistationRepository repository, IEmailService emailService, ICompetitionService competitionService, ICompetitionRepository competitionRepository, IUserService userService)
        {
            _repository = repository;
            _emailService = emailService;
            _competitionService = competitionService;
            _competitionRepository = competitionRepository;
            _userService = userService;
        }

        public async Task<IList<KoiRegistration>> GetListKoiRegistrationAsync()
        {
            return await _repository.ViewKoiRegistration();
        }
        public async Task<Response> AcceptKoiRegistration(int id, int competitionId)
        {
            var getKoiRegistration = await _repository.GetKoiRegistrationById(id);
            var getCompetition = await _competitionService.GetCompetitionById(competitionId);
            var getUser = await _userService.GetUserById(getKoiRegistration.UserId);
            if (getCompetition.Amount > 0)
            {
                if (getKoiRegistration.Status == KoiRegistrationCompetitionEnum.Rejected.ToString())
                {
                    return new Response() { Code = 1, Message = "Koi was rejected", Data = null };
                }
                if (getKoiRegistration.Status == KoiRegistrationCompetitionEnum.Accepted.ToString())
                {
                    return new Response() { Code = 1, Message = "Koi was accepted", Data = null };
                }
                getKoiRegistration.Status = KoiRegistrationCompetitionEnum.Accepted.ToString();
                getCompetition.Amount--;
                await _repository.UpdateKoiRegistration(getKoiRegistration);
                if (getCompetition.Amount == 0)
                {
                    var pendingKoiRegistrations = await _repository.GetListKoiPending(competitionId);
                    foreach (var pendingKoi in pendingKoiRegistrations)
                    {
                        var getUserPending = pendingKoi.UserId;
                        var getUserEmail = await _userService.GetUserById(getUserPending);
                        pendingKoi.Status = KoiRegistrationCompetitionEnum.Rejected.ToString();
                        await _repository.UpdateKoiRegistration(pendingKoi);
                        await _emailService.SendRejectKoiRegistrationAuto(getUserEmail.Email);
                    }
                }
                await _competitionRepository.UpdateCompetition(getCompetition);
                await _emailService.SendAcceptKoiRegistration(getUser.Email);
                return new Response() { Code = 0, Message = "Koi Accepted Successfully", Data = getKoiRegistration };
            }
            return new Response() { Code = 1, Message = "The competition is full.", Data = null };
        }
        public async Task<Response> RejectKoiRegistration(int id, string reason)
        {
            var getKoiRegistration = await _repository.GetKoiRegistrationById(id);
            var getUser = await _userService.GetUserById(getKoiRegistration.UserId);
            if (getKoiRegistration.Status == KoiRegistrationCompetitionEnum.Rejected.ToString())
            {
                return new Response() { Code = 1, Message = "Koi was rejected", Data = null };
            }
            if (getKoiRegistration.Status == KoiRegistrationCompetitionEnum.Accepted.ToString())
            {
                return new Response() { Code = 1, Message = "Koi was accepted", Data = null };
            }
            getKoiRegistration.Status = KoiRegistrationCompetitionEnum.Rejected.ToString();
            await _repository.UpdateKoiRegistration(getKoiRegistration);
            await _emailService.SendRejectKoiRegistration(getUser.Email, reason);
            return new Response() { Code = 0, Message = "Koi Reject Successfully", Data = getKoiRegistration };
        }
    }
}
