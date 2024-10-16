using Repository.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Dtos.Koi
{
    public class RegisterKoi
    {
        public string Name { get; set; }
        public string Variety { get; set; }
        public int Age { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; } // ID của người dùng đăng ký
    }
}
