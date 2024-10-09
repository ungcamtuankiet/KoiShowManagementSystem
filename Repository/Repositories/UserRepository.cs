using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Entites;
using Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly KoiShowManagementSystemContext _context;

        public UserRepository(KoiShowManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<User> AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<User> UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        // Giả định rằng chúng ta kiểm tra mật khẩu ở đây. Nếu sử dụng ASP.NET Identity, nó sẽ mã hóa và so sánh tự động.
        public Task<bool> VerifyPasswordAsync(User user, string password)
        {
            // Giả sử chúng ta không sử dụng ASP.NET Identity
            return Task.FromResult(user.Password == password); // So sánh mật khẩu đơn giản (lưu ý rằng đây là ví dụ)
        }
    }
}
