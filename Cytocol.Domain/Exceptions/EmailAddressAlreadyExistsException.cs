using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cytocol.Domain.Exceptions
{
    public class EmailAddressAlreadyExistsException : ApplicationException
    {
        public EmailAddressAlreadyExistsException(string msg = null, Exception innerException = null) : base(msg, innerException)
        {

        }
    }
}
