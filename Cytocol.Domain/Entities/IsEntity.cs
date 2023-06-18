using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cytocol.Domain.Entities
{
    public interface IsEntity
    {
        int Id { get; set; }
        bool IsDeleted { get; set; }

    }
}
