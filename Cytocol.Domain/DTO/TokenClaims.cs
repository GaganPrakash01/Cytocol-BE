using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cytocol.Domain.DTO
{
    public class TokenClaims
    {
        public int UserId { get; set; }
        public string Roles { get; set; }   
        public string Email { get; set; }    
    }
}
