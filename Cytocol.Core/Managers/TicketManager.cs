using Cytocol.Domain.DTO;
using Cytocol.Domain.Entities;
using Cytocol.Domain.Exceptions;
using Cytocol.Domain.Managers;
using Cytocol.Domain.Mappers;
using Cytocol.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cytocol.Core.Managers
{
    public class TicketManager : ITicketManager
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILawyerRepository _lawyerRepository;
        private readonly IObjectMapper _mapper;
        
        public TicketManager(ITicketRepository ticketRepository,IUserRepository userRepository,ILawyerRepository lawyerRepository,IObjectMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
            _lawyerRepository = lawyerRepository;
            _mapper = mapper;
        }

        public async Task<Ticket> AddTicket(TicketDto createTicket)
        {   
            try
            {
                var user = await _userRepository.FindById(createTicket.UserId);
                var ticket = _mapper.ConvertToTarget<TicketDto, Ticket>(createTicket);
                ticket.User = user;
                ticket.CreatedAt = DateTime.UtcNow;
                if(user.RaisedTickets == null)
                {
                    user.RaisedTickets = new List<Ticket>();
                    
                }
                if(user.RaisedTicketsIds == null)
                {
                    user.RaisedTicketsIds = new List<int>();
                }
                user.RaisedTickets.Add(ticket);
                user.RaisedTicketsIds.Add(ticket.Id);
                ticket.LawyerId = null;
                return await _ticketRepository.Save(ticket);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
            
        }

        public async Task<bool> EditTicket(TicketDto ticket, int ticketId)
        {
            var existingTicketId = await _ticketRepository.FindById(ticketId);
            if(existingTicketId == null)
            {
                throw new GenericNotFoundException<Ticket>(ticketId);
            }
            var user = await _userRepository.FindById(ticket.UserId);
            var lawyer = await _lawyerRepository.FindById(ticket.LawyerId);
            var existingTicket = await _ticketRepository.FindFirst(c => c.Id == ticketId);

            _mapper.MapToTarget<TicketDto, Ticket>(ticket, existingTicket);
            if(user!=null)
            {
                existingTicket.User = user;
            }
            if(lawyer!=null)
            {
                existingTicket.AssignedLawyer = lawyer;
            }
            if(lawyer.AssignedTickets == null)
            {
                lawyer.AssignedTickets = new List<Ticket>();
            }
            lawyer.AssignedTickets.Add(existingTicket);
            if (lawyer.AssignedTicketsIds == null)
            {
                lawyer.AssignedTicketsIds = new List<int>();
            }
            lawyer.AssignedTicketsIds.Add(ticketId);

            await _lawyerRepository.Update(lawyer);
            await _userRepository.Update(user);
            await _ticketRepository.Update(existingTicket);
            return true;
        }

        public Task<List<Ticket>> GetAllTickets()
        {
            return _ticketRepository.FindAll();
        }


        public async Task<Ticket> GetTicketByTicketId(int ticketId)
        {
            return await _ticketRepository.FindById(ticketId);
        }

        public async Task<List<Ticket>> GetTicketsByLawyerId(int lawyerId)
        {
            var tickets = await _ticketRepository.GetTicketsByLawyerId(lawyerId);
            return tickets;
        }

        public async Task<List<Ticket>> GetTicketsByUserId(int userId)
        {
            var tickets = await _ticketRepository.GetTicketsByUserId(userId);
            return tickets;
        }

        public Task<bool> RemoveTicket(int ticketId, int userId)
        {
            throw new NotImplementedException();
        }


    }
}
