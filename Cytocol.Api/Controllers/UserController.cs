using Cytocol.Domain.DTO;
using Cytocol.Domain.Managers;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Cytocol.Api.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private readonly IUserManager _userManager;

        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        // POST api/customer
        /// <summary>
        /// Creates a new customer with the provided information
        /// </summary>
        /// <param name="customer">The information of the customer to create</param>
        /// <returns>Status code with created customer data</returns>

        [HttpPost]
        [Route]
        public async Task<IHttpActionResult> CreateCustomer([FromBody] CreateUserDto user)
        {

            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var createdCustomer = await _userManager.CreateUser(user);
                return Ok(createdCustomer);
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

        // PUT api/customer
        /// <summary>
        /// Updates an existing customer with the provided information
        /// </summary>
        /// <param name="customer">The updated information of the customer</param>
        /// <returns>Status code with updated customer data</returns>

        [HttpPut]
        [Route("{userId}")]
        public async Task<IHttpActionResult> UpdateUser(int userId,[FromBody] CreateUserDto user)
        {
            try
            {
                var updatedCustomer = await _userManager.UpdateUser(userId,user);
                return Ok(updatedCustomer);
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/customer
        /// <summary>
        /// Gets all customers in the system
        /// </summary>
        /// <returns>Status code with list of all customers</returns>

        [HttpGet]
        [Route("all")]
        public async Task<IHttpActionResult> GetAllCustomer()
        {
            try
            {
                var customers = await _userManager.GetAllUsers();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/customer/5
        /// <summary>
        /// Gets the customer with the specified ID
        /// </summary>
        /// <param name="userId">The ID of the customer to get</param>
        /// <returns>Status code with customer data</returns>

        [HttpGet]
        [Route("{userId}")]

        public async Task<IHttpActionResult> GetById(int userId)
        {
            try
            {
                var customer = await _userManager.GetUserById(userId);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("{username}")]

        public async Task<IHttpActionResult> GetByUserName(string username)
        {
            try
            {
                var customer = await _userManager.GetUserByUserName(username);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/customer?username=johndoe
        /// <summary>
        /// Gets the customer with the specified username
        /// </summary>
        /// <param name="username">The username of the customer to get</param>
        /// <returns>Status code with customer data</returns>

        [HttpGet]
        [Route]
        public async Task<IHttpActionResult> GetByUserName2(string username)
        {
            try
            {
                var customer = await _userManager.GetUserByUserName(username);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
