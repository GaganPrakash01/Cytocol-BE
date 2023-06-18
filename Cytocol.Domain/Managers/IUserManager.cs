using Cytocol.Domain.DTO;
using Cytocol.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cytocol.Domain.Managers
{
    public interface IUserManager
    {
        Task<UserDto> CreateUser(CreateUserDto user);
        Task<User> UpdateUser(int userId,CreateUserDto user);
        Task<List<CreateUserDto>> GetAllUsers();
        Task<CreateUserDto> GetUserById(int userId);
        Task<User> GetUserByUserName(string username);

    }
}
