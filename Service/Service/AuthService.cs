using Microsoft.AspNetCore.Http;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task ClearSession()
        {
            _httpContextAccessor.HttpContext.Session.Clear();
        }

        public async Task<string> GetUserRole(string userRole)
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            return session.GetString("UserRole");
        }
    }
}
