using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cytocol.Domain.Exceptions.UserExceptions
{
    public class UserNameNotFoundException : ApplicationException
    {
        public UserNameNotFoundException(string msg) : base(msg) { }

    }
}
