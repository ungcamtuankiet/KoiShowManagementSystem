using Repository.Dtos.Response;
using Repository.Dtos.User;
using Repository.Entites;

namespace Service.IService
{
    public interface IUserService
    {
        Task<Response?> Login(LoginUserDto loginUserDto);
        Task<Response?> Register(RegisterUserDto registerUserDto);
        Task<IList<User>> GetAll();
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserById(int? userId);
    }
}
