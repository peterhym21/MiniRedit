using MiniReditRepository.Entities;
using MiniReditRepository.Interfaces;
using MiniReditServices.DTO;
using MiniReditServices.Interfaces;
using Service.Services;
using Service.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniReditServices.Services
{
    public class UsersServices : BaseService, IUsersServices
    {
        private readonly MappingService _mappingService;
        private readonly IUsersRepository _userRepository;

        public UsersServices(MappingService mappingService, IUsersRepository userRepository)
        {
            _mappingService = mappingService;
            _userRepository = userRepository;
        }

        public async Task<int> CreateUseres(UsersDTO userDTO)
        {
            try
            {
                int resultId = await _userRepository.CreateUseres(_mappingService._mapper.Map<Users>(userDTO));
                LogInformation($"Successfully created a new User : UserID {userDTO.UserId}, Fullname {userDTO.Name} :");
                return resultId;
            }
            catch (Exception ex)
            {
                LogError($"Failed to create a new User : UserID {userDTO.UserId}, Fullname {userDTO.Name} :", ex);
                return 1;

            }
        }

        public async Task DeleteUser(int userId)
        {
            try
            {
                await _userRepository.DeleteUser(userId);
                LogInformation($"Successfully removed a User : UserId {userId} :");
            }
            catch (Exception ex)
            {
                LogError($"Failed to remove a User : UserId {userId}", ex);

            }
        }

        public async Task<UsersDTO> GetUserById(int userId)
        {
            if (userId == 0)
            {
                return null;
            }
            try
            {
                UsersDTO user = _mappingService._mapper.Map<UsersDTO>(await _userRepository.GetUserById(userId));
                LogInformation($"Successfully fetched a User : UserId {user.UserId}, Fullname {user.Name} :");
                return user;
            }
            catch (Exception ex)
            {
                LogError($"Failed to fetch a User : UserId {userId}", ex);
                return null;

            }
        }

        public async Task<UsersDTO> GetUserlogin(string username, string password)
        {
            try
            {
                UsersDTO user = _mappingService._mapper.Map<UsersDTO>(await _userRepository.GetUserlogin(username, password));
                LogInformation($"Successfully fetched a User : UserName: {user.UserName} Fullname {user.Name} :");
                return user;
            }
            catch (Exception ex)
            {
                LogError($"Failed to fetch a User : UserName {username}", ex);
                return null;

            }
        }

        public async Task<List<UsersDTO>> GetUsers()
        {
            try
            {
                List<UsersDTO> users = _mappingService._mapper.Map<List<UsersDTO>>(await _userRepository.GetUsers());
                LogInformation($"Successfully fetched a list of Users");
                return users;
            }
            catch (Exception ex)
            {
                LogError("Failed to fetch a list of Users", ex);
                return null;

            }
        }

        public async Task<int> UpdateUser(UsersDTO userDTO)
        {
            try
            {
                int result = await _userRepository.UpdateUser(_mappingService._mapper.Map<Users>(userDTO));
                LogInformation($"Successfully updated a User : UserID { userDTO.UserId} :");
                return result;
            }
            catch (Exception ex)
            {
                LogError($"Failed to update a User : UserID { userDTO.UserId} :", ex);
                return 1;
            }
        }
    }
}
