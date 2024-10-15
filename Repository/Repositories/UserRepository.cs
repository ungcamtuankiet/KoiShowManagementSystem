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

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<bool> CheckEmailAndPhoneNo(string email, string phone)
        {
            var check = await _context.Users.FirstOrDefaultAsync(u => u.Email == email || u.PhoneNumber == phone);
            if (check == null)
            {
                return true;
            }
            return false;
        }

        public async Task<User?> GetUserByEmailAndPassword(string email, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task RegisterUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}
