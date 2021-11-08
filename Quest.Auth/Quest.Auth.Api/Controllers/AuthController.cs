using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [Consumes("application/x-www-form-urlencoded")]
        public IActionResult Token([FromForm] IFormCollection value)
        {
            return Ok();//return token
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public IActionResult Signup([FromBody] string value)
        {
            return Ok();
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] string value)
        {
            return Ok(); //return token
        }
        [HttpGet("[action]")]
        [Authorize(AuthorizationScope.Products.Get)]
        public IActionResult Validate()
        {
            return Ok();//return user
        }
        [HttpGet("[action]")]
        public IActionResult RefreshToken()
        {
            return Ok();
        }

    }
}
