using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cytocol.Domain.Exceptions.UserExceptions
{
    public class EmailExistsException : ApplicationException
    {
        public EmailExistsException(string msg) : base(msg) { }

    }
}
