using Cytocol.Domain.DTO;
using Cytocol.Domain.Entities;
using Cytocol.Domain.Exceptions;
using Cytocol.Domain.Exceptions.UserExceptions;
using Cytocol.Domain.Managers;
using Cytocol.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cytocol.Core.Managers
{
    public class AuthenticationManager : IAuthManager
    {
        private readonly ITokenService _tokenService;
        private readonly IPasswordManager _passwordManager;
        private readonly IUserManager _userManager;
        private readonly ILawyerManager _lawyerManager;

        public AuthenticationManager(
          ITokenService tokenService,
          IPasswordManager passwordManager,
          IUserManager userManager,
          ILawyerManager lawyerManager
          )
        {
            _tokenService = tokenService;
            _passwordManager = passwordManager;
            _userManager = userManager;
            _lawyerManager = lawyerManager;

        }

        public async Task<AuthenticationResponseDto> LoginUser(string username, string password)
        {
            try
            {
                var existingUser = await _userManager.GetUserByUserName(username);
                var hashedPassword = existingUser.Password;
                ValidatePassword(password, hashedPassword);
                return GetToken(existingUser.UserName, existingUser.Id, existingUser.Email, "User");
            }
            catch (UserNameNotFoundException ex)
            {
                try
                {
                    var existingAdmin = await _lawyerManager.GetLawyerByUserName(username);
                    var hashedAdminPassword = existingAdmin.Password;
                    ValidatePassword(password, hashedAdminPassword);
                    return GetToken(existingAdmin.UserName, existingAdmin.Id, existingAdmin.Email, "Lawyer");
                }
                catch (LawyerNotFoundException ex1)
                {
                    throw ex1;
                }
                throw new AuthenticationException(AuthenticationException.Cause.CREDENTIAL_VALIDATION_FAILED,
                    "Credential validation failed");
            }
        }

        public async Task<Dictionary<string, string>> NewTokenFromRefresh(string username)
        {
            try
            {
                var existingAdmin = await _lawyerManager.GetLawyerByUserName(username);
                return GetNewToken(existingAdmin.UserName, existingAdmin.Id, existingAdmin.Email, "Lawyer");
            }
            catch (LawyerNotFoundException ex)
            {
            }

            try
            {
                var existingUser = await _userManager.GetUserByUserName(username);

                return GetNewToken(existingUser.UserName, existingUser.Id, existingUser.Email, "User");
            }
            catch (Exception ex)
            {
            }
            throw new AuthenticationException(AuthenticationException.Cause.CREDENTIAL_VALIDATION_FAILED,
                "Credential validation failed");
        }

        private void ValidatePassword(string password, string hashPassword)
        {
            var isValid = _passwordManager.VerifyPassword(password, hashPassword);
            if (!isValid)
                throw new AuthenticationException(AuthenticationException.Cause.CREDENTIAL_VALIDATION_FAILED,
                    "Credential validation failed");
        }

        private AuthenticationResponseDto GetToken(string username, int userId, string email, string role)
        {
            var claims = new TokenClaims()
            {
                Email = email,
                Roles = role,
                UserId = userId
            };
            return new AuthenticationResponseDto()
            {
                AccessToken = _tokenService.GenerateToken(60, claims),
                RefreshToken = _tokenService.GenerateToken(3 * 24 * 60, claims),
                Username = username,
                UserId = userId,
                Type = "bearer",
                Role = role
            };
        }

       

        private Dictionary<string, string> GetNewToken(string username, int userId, string email, string role)
        {
            var claims = new TokenClaims()
            {
                Email = email,
                Roles = role,
                UserId = userId
            };
            return new Dictionary<string, string>
            {
                ["AccessToken"] = _tokenService.GenerateToken(60, claims),
                ["UserId"] = userId.ToString(),
                ["Type"] = "bearer",
                ["Username"] = username,
                ["Role"] = role
            };
        }
    }
}
