using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cytocol.Domain.Exceptions.UserExceptions
{
    public class UserIdNotFoundException : ApplicationException
    {
        public UserIdNotFoundException(string msg) : base(msg) { }

    }
}
