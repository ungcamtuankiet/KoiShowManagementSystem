using Repository.Entities;
namespace Repository.IRepositories
{
    public interface IKoiRegistationRepository
    {
        Task<IList<KoiRegistration>> ViewKoiRegistration();
        Task<IList<KoiRegistration>> GetKoiRegistationByCompetitionId(int competitionId);
        Task<IList<KoiRegistration>> GetListKoiPending(int competitionId);
        Task<KoiRegistration> GetKoiRegistrationById(int id);
        Task RegisterKoiRegistration(KoiRegistration registration);
        Task UpdateKoiRegistration(KoiRegistration registration);
    }
}




