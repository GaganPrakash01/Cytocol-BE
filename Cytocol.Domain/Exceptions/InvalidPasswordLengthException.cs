using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cytocol.Domain.Exceptions
{
    public class InvalidPasswordLengthException : ApplicationException
    {
        public InvalidPasswordLengthException(string msg = null) : base(msg)
        {

        }
    }
}
