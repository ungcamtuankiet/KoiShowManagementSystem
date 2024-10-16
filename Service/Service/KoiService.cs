
using Repository.Dtos.Koi;
using Repository.Dtos.Response;
using Repository.Dtos.User;
using Repository.Entites;
using Repository.Enum;
using Repository.IRepositories;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        public async Task<KoiFish> GetKoiById(int id)
        {
            var getKoi = await _koiRepository.GetKoiById(id);
            return getKoi;
        }

        public async Task<IEnumerable<KoiFish>> GetAllKoiFish()
        {
            return await _koiRepository.GetAllKoiFish();
        }
        public async Task<List<KoiFish>> GetKoiFishByUserIdAsync(int userId)
        {
            return await _koiRepository.GetKoiFishByUserIdAsync(userId);
        }
        public async Task<Response> RegisterKoi(RegisterKoi registerKoiDto, int? userId)
        {

            if (string.IsNullOrEmpty(registerKoiDto.Name) ||
            string.IsNullOrEmpty(registerKoiDto.Variety) ||
            registerKoiDto.Age <= 0 ||
            string.IsNullOrEmpty(registerKoiDto.Description))
            {
                return new Response()
                {
                    Code = 1,
                    Message = "Please fill in all information",
                    Data = null
                };
            }
            if (!IsValidAgeKoi(registerKoiDto.Age))
            {
                return new Response()
                {
                    Code = 1,
                    Message = "Age koi must be more than 0",
                    Data = null
                };
            }
            var koi = new KoiFish
            {
                Name = registerKoiDto.Name,
                Variety = registerKoiDto.Variety,
                Age = registerKoiDto.Age,
                Description = registerKoiDto.Description,
                RegistrationDate = DateTime.Now,
                CreatedAt = DateTime.Now,
                Status = KoiStatus.Avtive.ToString(),
                UserId = userId // ID của người dùng đăng ký
            };
            await _koiRepository.AddKoiRegistration(koi);
            return new Response()
            {
                Code = 0,
                Message = "Koi registered successfully",
                Data = null
            };
        }

        public async Task<IEnumerable<KoiFish>> GetKoiForCompetition(int competitionId)
        {
            return await _koiRepository.GetAllKoiForCompetition(competitionId);
        }

        public async Task<Response> DeleteKoi(int id)
        {
            var getKoi = await _koiRepository.GetKoiById(id);
            if (getKoi != null)
            {
                await _koiRepository.DeleteKoi(id);
                return new Response()
                {
                    Code = 0,
                    Message = "Delete Koi Successfully",
                    Data = null
                };
            }
            return new Response()
            {
                Code = 1,
                Message = "Koi is not exist",
                Data = null
            };
        }

        public async Task<Response> UpdateKoi(KoiFish koiFish)
        {
            var getKoi = await _koiRepository.GetKoiById(koiFish.Id);
            if (getKoi != null)
            {
                await _koiRepository.UpdateKoi(getKoi);
                return new Response()
                {
                    Code = 0,
                    Message = "Update Koi Successfully",
                    Data = null
                };
            }
            return new Response()
            {
                Code = 1,
                Message = "Koi is not exist",
                Data = null
            };
        }

        private bool IsValidAgeKoi(int ageKoi)
        {
            return ageKoi > 0;
        }
    }
}
