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
            try
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
                    if (competition.Amount <= 0 || competition.Amount % 2 != 0)
                    {
                        return new Response { Code = 1, Message = "Amount of User must be more than 0 and should be even", Data = null };
                    }
                    competition.CreatedAt = DateTime.Now;
                    competition.Status = StatusShowEnum.InProgess.ToString();
                    await _competitionRepository.CreateCompetition(competition);
                    return new Response { Code = 0, Message = "Create Competition Successfully", Data = competition };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
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
            try
            {
                await _competitionRepository.DeleteCompetition(competition);
                return new Response() { Code = 0, Message = "Delete Competition Successfully", Data = null };
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public async Task<Response> StartCompetition(int id)
        {
            var getCompetiton = await _competitionRepository.GetCompetitionById(id);
            var getKoiRegistation = await _koiRegistationRepository.GetKoiRegistationByCompetitionId(id);
            if(getCompetiton.Amount > 0)
            {
                return new Response() { Code = 1, Message = $"Còn thiếu {getCompetiton.Amount} để bắt đầu cuộc thi", Data = null };
            }
            // Phân bố cá Koi theo từng cặp đấu
            var koiRegistrations = getKoiRegistation.ToList();
            var random = new Random();
            koiRegistrations = koiRegistrations.OrderBy(x => random.Next()).ToList();

            // Tạo các cặp đấu
            List<Tuple<KoiRegistration, KoiRegistration>> matchups = new List<Tuple<KoiRegistration, KoiRegistration>>();
            for (int i = 0; i < koiRegistrations.Count; i += 2)
            {
                if (i + 1 < koiRegistrations.Count)
                {
                    matchups.Add(new Tuple<KoiRegistration, KoiRegistration>(koiRegistrations[i], koiRegistrations[i + 1]));
                }
            }

            // Cập nhật trạng thái của cuộc thi
            getCompetiton.Status = StatusShowEnum.Starting.ToString();

            // Lưu các thay đổi vào database
            await _competitionRepository.UpdateCompetition(getCompetiton);

            // Logic xử lý vòng đấu: Tứ Kết, Bán Kết, Chung Kết sẽ được thêm sau
            // Ví dụ: Tạo các vòng đấu, lưu kết quả và tiến hành theo quy tắc loại trực tiếp
            return new Response() { Code = 0, Message = "Start Competition Successfully", Data = matchups };
        }
    }
}
