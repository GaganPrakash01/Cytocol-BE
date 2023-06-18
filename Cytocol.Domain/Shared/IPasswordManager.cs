using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cytocol.Domain.Shared
{
    public interface IPasswordManager
    {
        string HashPassword(string password, string salt);
        string GenerateSalt();
        bool VerifyPassword(string plainPassword, string hashPassword);
        string GenerateRandomPassword(int passwordLength = 8);
    }
}
