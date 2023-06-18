using Cytocol.Domain.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;

namespace Cytocol.Api.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        private readonly IAuthManager _authManager;
        public AuthController(IAuthManager authManager, ILawyerManager lawyerManager)
        {
            _authManager = authManager;
        }

        [HttpPost]
        [Route("token")]
        //[RateLimit(5,60)]
        public async Task<IHttpActionResult> GetToken(FormDataCollection formData) => Ok(await _authManager.LoginUser(
            formData.Get("username"),
            formData.Get("password"))
        );

        [HttpPost]
        [Route("refresh")]
        //[RateLimit(5, 60)]
        public async Task<IHttpActionResult> GetTokenFromRefresh() => Ok(await _authManager.NewTokenFromRefresh(User.Identity.Name));

    }
}
