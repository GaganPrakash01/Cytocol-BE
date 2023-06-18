using Cytocol.Core.Managers;
using Cytocol.Domain.DTO;
using Cytocol.Domain.Entities;
using Cytocol.Domain.Managers;
using Cytocol.Domain.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Cytocol.Api.Controllers
{
    [System.Web.Http.RoutePrefix("api/lawyer")]
    public class LawyerController : ApiController
    {
        private readonly ILawyerManager _lawyerManager;
        private readonly IObjectMapper _mapper;
        public LawyerController(ILawyerManager lawyerManager, IObjectMapper mapper)
        {
            _lawyerManager = lawyerManager;
            _mapper = mapper;
        }


        //Method to create an admin
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route]
        public async Task<IHttpActionResult> CreateLawyer(LawyerCreatedDto lawyer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newLawyer = await _lawyerManager.CreateLawyer(lawyer);
            return Created($"api/admin/{newLawyer.Id}", _mapper.ConvertToTarget<Lawyer, LawyerCreatedDto>(newLawyer));
        }

        //Method to get all admins
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route]
        public async Task<IHttpActionResult> GetAllAdmins() => Ok(
            await _lawyerManager.GetAllLawyers());

        // Method to update a given admin by their id
        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("{userId}")]
        public async Task<IHttpActionResult> UpdateLawyer(int userId,[FromBody] CreateUserDto lawyer)
        {
            try
            {
                var updatedLawyer = await _lawyerManager.UpdateLawyer(userId,lawyer);
                return Ok(updatedLawyer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("{userId}")]

        public async Task<IHttpActionResult> GetById(int userId)
        {
            try
            {
                var lawyer = await _lawyerManager.GetLawyerById(userId);
                return Ok(lawyer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}