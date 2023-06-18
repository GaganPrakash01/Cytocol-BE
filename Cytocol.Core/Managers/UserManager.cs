using Cytocol.Domain.DTO;
using Cytocol.Domain.Entities;
using Cytocol.Domain.Exceptions.UserExceptions;
using Cytocol.Domain.Managers;
using Cytocol.Domain.Mappers;
using Cytocol.Domain.Repositories;
using Cytocol.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cytocol.Core.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository UserRepository;
        private readonly IObjectMapper _mapper;
        private readonly IPasswordManager _passwordManager;

        public UserManager(IUserRepository userRepository, IPasswordManager passwordManager, IObjectMapper mapper)
        {
            UserRepository = userRepository;
            _mapper = mapper;
            _passwordManager = passwordManager;
        }

        public async Task<UserDto> CreateUser(CreateUserDto Createuser)
        {
            //Checks if the customer exists with the provided email.
            var existingUser = await UserRepository.FindFirst(c => c.Email == Createuser.Email);
            if (existingUser != null)
            {
                throw new UserNameNotFoundException("Email already exists.");
            }
            // Converts the DTO to an entity and set the password.
            var user = _mapper.ConvertToTarget<CreateUserDto, User>(Createuser);
            var password = _passwordManager.GenerateRandomPassword();
            user.Password = "password123";
            user.UserName = user.Email;
            user.AccountActive = true;
            // Set additional fields for the customer.
            var userDto = _mapper.ConvertToTarget<User, UserDto>(user);
            user.Password = _passwordManager.HashPassword(user.Password, _passwordManager.GenerateSalt());

            //Saves the customer to the database and returns the DTO.
            await UserRepository.Save(user);
            return userDto;
        }

        public async Task<List<CreateUserDto>> GetAllUsers()
        {
            // Retrieves all Users from the database and convert to DTOs.
            var originalUsers = await UserRepository.FindAll();
            var users = _mapper.ConvertToTarget<List<User>, List<CreateUserDto>>(originalUsers);
            return users;
        }

        public async Task<CreateUserDto> GetUserById(int userId)
        {
            //Check if the customer exists with the provided ID.
            var existingUserId = await UserRepository.FindFirst(c => c.Id == userId);
            if (existingUserId == null)
            {
                throw new UserIdNotFoundException("Id not found");
            }
            //Retrieve the customer from the database and convert to a DTO.
            var existingUser = await UserRepository.FindById(userId);
            var user = _mapper.ConvertToTarget<User, CreateUserDto>(existingUser);
            return user;
        }

        public async Task<User> GetUserByUserName(string username)
        {
            // Checks if a customer exists with the provided username
            var existingUserName = await UserRepository.FindFirst(c => c.Email == username);
            if (existingUserName == null)
            {
                throw new UserNameNotFoundException("Username not found");
            }
            return await UserRepository.FindFirst(user => user.Email == username);

        }

        public async Task<User> UpdateUser(int userId,CreateUserDto user)
        {
            var existingUser = await UserRepository.FindFirst(c => c.Id == userId);
            _mapper.MapToTarget<CreateUserDto, User>(user, existingUser);
            return await UserRepository.Update(existingUser);

        }
    }
}
