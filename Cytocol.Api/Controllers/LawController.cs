using Cytocol.Core.Managers;
using Cytocol.Domain.DTO;
using Cytocol.Domain.Entities;
using Cytocol.Domain.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;

namespace Cytocol.Api.Controllers
{
    [RoutePrefix("api/law")]
    public class LawController : ApiController
    {
        private readonly ILawManager _lawManager;
        public LawController(ILawManager lawManager)
        {
            _lawManager = lawManager;
        }

        [HttpPost]
        [Route]
        public async Task<IHttpActionResult> createLaw(Law law)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var createdCustomer = await _lawManager.AddLaw(law);
                return Ok(createdCustomer);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{lawId}")]
        public async Task<IHttpActionResult> UpdateLaw([FromBody] Law law)
        {
            try
            {
                var updatedLaw = await _lawManager.UpdateLaw(law);
                return Ok(updatedLaw);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("all")]
        public async Task<IHttpActionResult> GetAllLaws()
        {   
            try
            {
                var laws = await _lawManager.GetAllLaws();
                return Ok(laws);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        
    }
}
