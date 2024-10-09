
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

        public async Task<Response> GetListKoiFish()
        {
            var getListKoi = await _koiRepository.GetListKoiFish();
            if (getListKoi != null)
            {
                return new Response
                {
                    Code = 0,
                    Message = "",
                    Data = getListKoi
                };
            }
            return new Response
            {
                Code = 0,
                Message = "",
                Data = null
            };
        }

        public async Task<Response> AddNewKoi(RegisterKoi registerKoi)
        {
            var newKoi = new KoiFish()
            {
                UserId = 1,
                Name = registerKoi.Name,
                Variety = registerKoi.Variety,
                Description = registerKoi.Description,
                RegistrationDate = DateTime.Now,
                Status = "Pending"
            };
            await _koiRepository.RegisterKoi(newKoi);
            return new Response
            {
                Code = 0,
                Message = "Add New Koi Successfully",
                Data = newKoi
            };
        }
    }
}
