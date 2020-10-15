using MiniReditServices.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniReditServices.Interfaces
{
    public interface IUsersServices
    {
        Task<List<UsersDTO>> GetUsers();
        Task<UsersDTO> GetUserlogin(string username, string password);
        Task<UsersDTO> GetUserById(int userId);
        Task<int> CreateUseres(UsersDTO userDTO);
        Task<int> UpdateUser(UsersDTO userDTO);
        Task DeleteUser(int Userid);
    }
}
