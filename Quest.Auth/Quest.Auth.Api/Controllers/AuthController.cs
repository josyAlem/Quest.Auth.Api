using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quest.Auth.Common.Request;
using Quest.Auth.Common.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Quest.Auth.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {

       
        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<ActionResult<SignupResponse>> Signup([FromBody]  SignupRequest signupRequest)
        {
            return Ok(new SignupResponse());
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest loginRequest)
        {
            return Ok(new LoginResponse()); 
        }
       
        [HttpGet("[action]")]
        public async Task<ActionResult<LoginResponse>> Refresh([FromBody] RefreshTokenRequest refreshTokenRequest)
        {
            return Ok(new LoginResponse());
        }

        [HttpGet("[action]")]
        [Authorize(AuthorizationScope.Products.Get)]
        public IActionResult Validate()
        {
            return Ok();//return user
        }
    }
}
