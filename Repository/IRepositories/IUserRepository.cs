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
        Task<User?> GetUserByEmailAndPassword(string email, string password);
        Task<User> GetUserByEmail(string email);
        Task<bool> CheckEmailAndPhoneNo(string email, string phone);
        Task<User?> GetUserById(int id);
        Task RegisterUser(User user);
    }
}
