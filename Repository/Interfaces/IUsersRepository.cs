using MiniReditRepository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniReditRepository.Interfaces
{
    public interface IUsersRepository
    {
        Task<List<Users>> GetUsers();
        Task<Users> GetUserlogin(string username, string password);
        Task<Users> GetUserById(int userId);
        Task<int> CreateUseres(Users userEn);
        Task<int> UpdateUser(Users userEn);
        Task DeleteUser(int Userid);
    }
}
