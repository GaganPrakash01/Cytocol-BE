using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cytocol.Domain.Exceptions
{
    public class AuthenticationException : ApplicationException
    {
        public Cause RejectCause;
        public AuthenticationException(Cause cause, string message = null) : base(message)
        {
            RejectCause = cause;
        }

        public enum Cause
        {
            CREDENTIAL_VALIDATION_FAILED = 401,
        }
    }
}
