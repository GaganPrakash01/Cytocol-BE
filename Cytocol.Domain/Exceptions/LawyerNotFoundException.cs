using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cytocol.Domain.Exceptions
{
    public class LawyerNotFoundException : ApplicationException
    {
        public LawyerNotFoundException(string msg = null, Exception innerException = null) : base(msg, innerException)
        {

        }
    }
}
