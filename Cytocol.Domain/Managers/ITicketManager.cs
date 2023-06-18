using Cytocol.Domain.DTO;
using Cytocol.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cytocol.Domain.Managers
{
    public interface ITicketManager
    {
        Task<Ticket> AddTicket(TicketDto ticket);
        Task<bool> RemoveTicket(int ticketId, int userId);
        Task<Ticket> GetTicketByTicketId(int ticketId);
        Task<List<Ticket>> GetAllTickets();
        Task<List<Ticket>> GetTicketsByUserId(int userId);
        Task<List<Ticket>> GetTicketsByLawyerId(int lawyerId);
        Task<bool> EditTicket(TicketDto ticket, int ticketId);

    }
}
