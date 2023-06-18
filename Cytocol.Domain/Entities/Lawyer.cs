using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cytocol.Domain.Entities
{
    public class Lawyer : BaseUser
    {
        public virtual List<Ticket> AssignedTickets { get; set; }
        public List<int> AssignedTicketsIds { get; set; }

    }
}
