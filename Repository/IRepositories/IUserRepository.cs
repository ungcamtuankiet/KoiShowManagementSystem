using Repository.Entities;
namespace Repository.IRepositories
{
    public interface IUserRepository
    {
        Task<IList<User>> GetAll();
        Task<User?> GetUserByEmailAndPassword(string email, string password);
        Task<User> GetUserByEmail(string email);
        Task<bool> CheckEmailAndPhoneNo(string email, string phone);
        Task<User?> GetUserById(int? id);
        Task RegisterUser(User user);
    }
}


