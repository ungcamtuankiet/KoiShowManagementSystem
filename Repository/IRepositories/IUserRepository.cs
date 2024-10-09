using Repository.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepositories
{
    public interface IUserRepository
    {
        Task<User> AddUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task<User?> GetUserByEmailAsync(string email);
        Task<bool> VerifyPasswordAsync(User user, string password);
    }
}
