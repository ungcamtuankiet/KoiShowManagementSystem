using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Dtos.User
{
    public class RegisterUserDto
    {
        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        public string Password { get; set; } = null!;
    }
}
