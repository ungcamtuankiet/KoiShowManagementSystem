using Repository.Dtos.Response;
using Repository.Dtos.User;
using Repository.Entites;
using Repository.Enum;
using Repository.IRepositories;
using Service.IService;
using System.Data;

namespace Service.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> GetUserByEmail(string email)
        {
            return await _userRepository.GetUserByEmail(email);
        }
        public async Task<Response?> Login(LoginUserDto loginUserDto)
        {
            var login = await _userRepository.GetUserByEmailAndPassword(loginUserDto.Email, loginUserDto.Password);
            if (loginUserDto.Email == null || loginUserDto.Password == null)
            {
                return new Response()
                {
                    Code = 1,
                    Message = "Email and password cannot be blank",
                    Data = null
                };
            }
            if (login == null)
            {
                return new Response()
                {
                    Code = 1,
                    Message = "Email or Password incorrect",
                    Data = null
                };
            }
            return new Response()
            {
                Code = 0,
                Message = "Login Successfully",
                Data = null
            };
        }

        public async Task<Response> Register(RegisterUserDto registerUserDto)
        {
            var checkEmailAndPhoneNo = await _userRepository.CheckEmailAndPhoneNo(registerUserDto.Email, registerUserDto.PhoneNumber);
            if (registerUserDto.Email == null || registerUserDto.PhoneNumber == null || registerUserDto.FullName == null || registerUserDto.Address == null || registerUserDto.Password == null)
            {
                return new Response()
                {
                    Code = 1,
                    Message = "Please fill in all information",
                    Data = null
                };
            }
            if (!checkEmailAndPhoneNo)
            {
                return new Response()
                {
                    Code = 1,
                    Message = "Email or PhoneNumber already exist",
                    Data = null
                };
            }
            // Kiểm tra định dạng Email
            if (!IsValidEmail(registerUserDto.Email))
            {
                return new Response()
                {
                    Code = 1,
                    Message = "Invalid email format",
                    Data = null
                };
            }

            // Kiểm tra định dạng PhoneNumber
            if (!IsValidPhoneNumber(registerUserDto.PhoneNumber))
            {
                return new Response()
                {
                    Code = 1,
                    Message = "Phone number must be between 10 and 15 digits",
                    Data = null
                };
            }

            // Kiểm tra định dạng Password
            if (!IsValidPassword(registerUserDto.Password))
            {
                return new Response()
                {
                    Code = 1,
                    Message = "Password must be between 6 and 20 characters, and include uppercase, lowercase, numbers, and special characters.",
                    Data = null
                };
            }
            else
            {
                var user = new User
                {
                    FullName = registerUserDto.FullName,
                    Email = registerUserDto.Email,
                    Password = registerUserDto.Password,
                    PhoneNumber = registerUserDto.PhoneNumber,
                    Address = registerUserDto.Address,
                    Role = RoleEnum.Member.ToString(),
                    Status = "Active",
                    CreatedAt = DateTime.Now
                };
                await _userRepository.RegisterUser(user);
                return new Response()
                {
                    Code = 0,
                    Message = "Register Successully",
                    Data = null
                };
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var mail = new System.Net.Mail.MailAddress(email);
                return mail.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // Phương thức kiểm tra định dạng số điện thoại
        private bool IsValidPhoneNumber(string phoneNumber)
        {
            return phoneNumber.Length >= 10 && phoneNumber.Length <= 15 && phoneNumber.All(char.IsDigit);
        }

        // Phương thức kiểm tra định dạng mật khẩu
        private bool IsValidPassword(string password)
        {
            // Kiểm tra độ dài và yêu cầu ký tự
            return password.Length >= 6 && password.Length <= 20 &&
                   password.Any(char.IsUpper) &&  // Chứa ít nhất một chữ cái in hoa
                   password.Any(char.IsLower) &&  // Chứa ít nhất một chữ cái thường
                   password.Any(char.IsDigit) &&   // Chứa ít nhất một số
                   password.Any(ch => !char.IsLetterOrDigit(ch)); // Chứa ít nhất một ký tự đặc biệt
        }
    }
}
