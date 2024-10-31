using Microsoft.EntityFrameworkCore;
using Repository.Entities;
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
        public async Task<IList<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
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

        public async Task<User?> GetUserById(int? id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task RegisterUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUser(User user)
        {
            _context.Users.Remove(user);   
            await _context.SaveChangesAsync();
        }
    }
}
