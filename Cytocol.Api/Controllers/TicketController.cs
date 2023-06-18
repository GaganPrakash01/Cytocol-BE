using Cytocol.Core.Managers;
using Cytocol.Domain.DTO;
using Cytocol.Domain.Entities;
using Cytocol.Domain.Managers;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;

namespace Cytocol.Api.Controllers
{
    [RoutePrefix("api/ticket")]
    public class TicketController : ApiController
    {
        private readonly ITicketManager _ticketManager;

        public TicketController(ITicketManager ticketManager)
        {
            _ticketManager = ticketManager;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IHttpActionResult> GetAllTickets()
        {
            try
            {
                var tickets = await _ticketManager.GetAllTickets();
                return Ok(tickets);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("{ticketId}")]

        public async Task<IHttpActionResult> GetById(int ticketId)
        {
            try
            {
                var ticket = await _ticketManager.GetTicketByTicketId(ticketId);
                return Ok(ticket);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("user/{userId}")]

        public async Task<IHttpActionResult> GetAllTicketsByUserId(int userId)
        {
            try
            {
                var tickets = await _ticketManager.GetTicketsByUserId(userId);
                return Ok(tickets);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("lawyer/{lawyerId}")]

        public async Task<IHttpActionResult> GetAllTicketsByLawyerId(int lawyerId)
        {
            try
            {
                var tickets = await _ticketManager.GetTicketsByLawyerId(lawyerId);
                return Ok(tickets);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route]
        public async Task<IHttpActionResult> CreateTicket([FromBody] TicketDto ticket)
        {

            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var createdTicket = await _ticketManager.AddTicket(ticket);
                return Ok(createdTicket);
            }
            catch (DbEntityValidationException ex)
            {
                string message = "";

                foreach (var result in ex.EntityValidationErrors)
                {
                    foreach (var error in result.ValidationErrors)
                    {
                        message += "Property {0}: Error {1}" + error.PropertyName + error.ErrorMessage;
                    }
                }

                return BadRequest(message);
            }
            catch (System.ArgumentException ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{ticketId}")]
        public async Task<IHttpActionResult> UpdateTicket(int ticketId, [FromBody] TicketDto ticket)
        {
            try
            {
                var updatedTicket = await _ticketManager.EditTicket(ticket, ticketId);
                return Ok(updatedTicket);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }

    
}
