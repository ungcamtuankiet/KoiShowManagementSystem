using Repository.Dtos.Response;
using Repository.Dtos.User;
using Repository.Entites;
using Repository.IRepositories;
using Service.IService;

namespace Service.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Response> RegisterUserAsync(RegisterUserDto userDto)
        {
            var checkExistUser = _userRepository.GetUserByEmailAsync(userDto.Email);
            // Kiểm tra nếu người dùng đã tồn tại
            if (checkExistUser != null)
            {
                return new Response
                {
                    Code = 1,
                    Message = "User already exist",
                    Data = null
                };
            }
            var newUser = new User
            {
                FullName = userDto.FullName,
                Email = userDto.Email,
                Password = userDto.Password,
                PhoneNumber = userDto.PhoneNumber,
                Address = userDto.Address,
                Role = 1
            };
            // Thêm người dùng vào cơ sở dữ liệu
            await _userRepository.AddUserAsync(newUser);
            return new Response
            {
                Code = 0,
                Message = "Add user successfully",
                Data = newUser
            };
        }

        public async Task<Response> UpdateUserAsync(UpdateUser updateUser)
        {
            var newUser = new User
            {
                FullName = updateUser.FullName,
                Email = updateUser.Email,
                Password = updateUser.Password,
                PhoneNumber = updateUser.PhoneNumber,
                Address = updateUser.Address,
            };
            await _userRepository.UpdateUserAsync(newUser);
            return new Response
            {
                Code = 0,
                Message = "Update User Successfully",
                Data = newUser
            };
        }


        public async Task<Response> LoginAsync(LoginUserDto userDto)
        {
            // Tìm người dùng qua email
            var user = await _userRepository.GetUserByEmailAsync(userDto.Email);
            if (user == null)
            {
                return new Response
                {
                    Code = 1,
                    Message = "User not exist",
                    Data = user
                };
            }

            // Kiểm tra mật khẩu
            var passwordValid = await _userRepository.VerifyPasswordAsync(user, userDto.Password);
            return new Response
            {
                Code = 0,
                Message = "",
                Data = user
            };
        }
    }
}
