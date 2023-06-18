using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cytocol.Domain.Exceptions
{
    public class GenericNotFoundException<TEntity> : ApplicationException
    {
        public GenericNotFoundException(int Id) : base($"{typeof(TEntity).Name} of Id {Id} not found") { }
    }
}
