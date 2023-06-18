using Cytocol.Data.Context;
using Cytocol.Domain.Entities;
using Cytocol.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cytocol.Data.Repositories
{
    public class TicketRepository : BaseRepository<Ticket>,ITicketRepository
    {
        public TicketRepository(CytocolDbContext context) : base(context)
        {
        }

        public async Task<List<Ticket>> GetTicketsByUserId(int userId)
        {
            return await _context.Tickets.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<List<Ticket>> GetTicketsByLawyerId(int lawyerId)
        {
            return await _context.Tickets.Where(x => x.LawyerId == lawyerId).ToListAsync();
        }

       
    }
}
