using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cytocol.Domain.Entities
{
    public class User : BaseUser
    {
        public virtual List<Ticket> RaisedTickets { get; set; }
        public List<int> RaisedTicketsIds { get; set; }
    }
}
