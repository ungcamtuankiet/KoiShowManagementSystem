
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
        private readonly IFileService _fileService;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        public KoiService(IKoiRepository repository, IFileService fileService, IUserService userService, IEmailService emailService)
        {
            _koiRepository = repository;
            _fileService = fileService;
            _userService = userService;
            _emailService = emailService;
        }

        public async Task<KoiFish> GetKoiById(int id)
        {
            var getKoi = await _koiRepository.GetKoiById(id);
            return getKoi;
        }

        public async Task<IList<KoiFish>> GetAllKoiFish()
        {
            return await _koiRepository.GetAllKoiFish();
        }
        public async Task<List<KoiFish>> GetKoiFishByUserIdAsync(int userId)
        {
            return await _koiRepository.GetKoiFishByUserIdAsync(userId);
        }
        public async Task<Response> RegisterKoi(RegisterKoi registerKoiDto, int? userId)
        {
            var getUser = await _userService.GetUserById(userId);
            string avatarUrl = null;
            if (string.IsNullOrEmpty(registerKoiDto.Name) || string.IsNullOrEmpty(registerKoiDto.Variety.ToString()) || registerKoiDto.Size <= 0 || string.IsNullOrEmpty(registerKoiDto.Description) || registerKoiDto.Avatar == null)
            {
                if (registerKoiDto.Size <= 0)
                {
                    return new Response()
                    {
                        Code = 1,
                        Message = "Size koi must be more than 0 and not null",
                        Data = null
                    };
                }
                return new Response()
                {
                    Code = 1,
                    Message = "Please fill in all information",
                    Data = null
                };
            }

            // Kiểm tra file ảnh
            avatarUrl = await _fileService.SaveKoiAvatar(registerKoiDto.Avatar);
            var koi = new KoiFish
            {
                Name = registerKoiDto.Name,
                Variety = KoiVariety.Kohaku.ToString(),
                Size = registerKoiDto.Size,
                AvatarUrl = avatarUrl,
                Description = registerKoiDto.Description,
                RegistrationDate = DateTime.Now,
                CreatedAt = DateTime.Now,
                Status = KoiStatus.Pending.ToString(),
                UserId = userId // ID của người dùng đăng ký
            };
            await _koiRepository.AddKoiRegistration(koi);
            await _emailService.SendEmailAddNewKoi(getUser.Email);
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
                // Nếu có avatar, xóa tấm hình trước khi xóa KoiFish
                if (!string.IsNullOrEmpty(getKoi.AvatarUrl))
                {
                    // Gọi dịch vụ xóa file
                    await _fileService.DeleteImage(getKoi.AvatarUrl);
                }
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

        public async Task<Response> UpdateKoi(UpdateKoiDto updateKoi, int id)
        {
            var getKoi = await _koiRepository.GetKoiById(id);
            string avatarUrl = null;
            if (string.IsNullOrEmpty(updateKoi.Name) || string.IsNullOrEmpty(updateKoi.Variety.ToString()) || updateKoi.Size <= 0 || string.IsNullOrEmpty(updateKoi.Description) || updateKoi.Avatar == null)
            {
                if (updateKoi.Size <= 0)
                {
                    return new Response()
                    {
                        Code = 1,
                        Message = "Size koi must be more than 0 and not null",
                        Data = null
                    };
                }
                return new Response()
                {
                    Code = 1,
                    Message = "Please fill in all information",
                    Data = null
                };
            }
            
            // Gọi phương thức để lưu file và nhận đường dẫn
            avatarUrl = await _fileService.SaveKoiAvatar(updateKoi.Avatar);
            if (getKoi != null)
            {
                getKoi.Name = updateKoi.Name;
                getKoi.Variety = updateKoi.Variety;
                getKoi.Size = updateKoi.Size;
                getKoi.Description = updateKoi.Description;
                getKoi.UpdatedAt = DateTime.Now;
                getKoi.AvatarUrl = avatarUrl;
                getKoi.Status = KoiStatus.Pending.ToString();
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
    }
}
