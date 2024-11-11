using Repository.Entities;
namespace Repository.IRepositories
{
    public interface IKoiRepository
    {
        Task<KoiFish?> GetKoiById(int koiId);
        Task<IList<KoiFish>> GetAllKoiFish();
        Task<List<KoiFish>> GetAllKoiFishForStaff();
        Task<List<KoiFish>> GetKoiFishByUserIdAsync(int userId);
        Task<IEnumerable<KoiFish>> GetAllKoiForCompetition(int competitionId);
        Task AddKoiRegistration(KoiFish koiFish);
        Task DeleteKoi(int id);
        Task UpdateKoi(KoiFish koiFish);
    }
}


