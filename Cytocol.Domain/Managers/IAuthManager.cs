using Cytocol.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cytocol.Domain.Managers
{
    public interface IAuthManager
    {
        Task<AuthenticationResponseDto> LoginUser(string username, string password);
        Task<Dictionary<string, string>> NewTokenFromRefresh(string username);

    }
}
