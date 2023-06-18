using Cytocol.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Cytocol.Domain.Shared
{
    public interface ITokenService
    {
        string GenerateToken(int expiryInMinutes, params TokenClaims[] claims);
        ClaimsPrincipal VerifyToken(string token);
    }
}
