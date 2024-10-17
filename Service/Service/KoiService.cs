
using Microsoft.AspNetCore.Http;
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
            // Kiểm tra file ảnh
            string avatarUrl = null;
            if (registerKoiDto.Avatar != null)
            {
                // Gọi phương thức để lưu file và nhận đường dẫn
                avatarUrl = await SaveKoiAvatar(registerKoiDto.Avatar);
            }
            var koi = new KoiFish
            {
                Name = registerKoiDto.Name,
                Variety = registerKoiDto.Variety,
                Age = registerKoiDto.Age,
                AvatarUrl = avatarUrl,
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

        public async Task<Response> UpdateKoi(UpdateKoi updateKoi, int id)
        {
            var getKoi = await _koiRepository.GetKoiById(id);
            if (getKoi != null)
            {
                getKoi.Name = updateKoi.Name;
                getKoi.Variety = updateKoi.Variety;
                getKoi.Age = updateKoi.Age;
                getKoi.Description = updateKoi.Description;
                getKoi.UpdatedAt = DateTime.Now;
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

        private async Task<string> SaveKoiAvatar(IFormFile avatar)
        {
            // Đường dẫn thư mục lưu ảnh
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/koi");

            // Đảm bảo thư mục tồn tại
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // Tạo tên file duy nhất dựa trên thời gian và tên gốc của file
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + avatar.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // Lưu file vào thư mục
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await avatar.CopyToAsync(fileStream);
            }

            // Trả về đường dẫn URL để lưu vào cơ sở dữ liệu
            return "/images/koi/" + uniqueFileName; // Đường dẫn tương đối
        }
    }
}
