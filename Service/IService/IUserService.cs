using Repository.Dtos.Response;
using Repository.Dtos.User;

namespace Service.IService
{
    public interface IUserService
    {
        Task<Response> RegisterUserAsync(RegisterUserDto userDto);
        Task<Response> LoginAsync(LoginUserDto userDto);
        Task<Response> UpdateUserAsync(UpdateUser updateUser);
    }
}
