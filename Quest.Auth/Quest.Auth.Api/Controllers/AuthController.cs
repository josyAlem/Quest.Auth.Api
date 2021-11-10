using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quest.Auth.Common.Request;
using Quest.Auth.Common.Response;
using Quest.Auth.Services.Interfaces;
using System.Threading.Tasks;


namespace Quest.Auth.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
       
        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<ActionResult<SignupResponse>> Signup([FromBody]  SignupRequest signupRequest)
        {
            var response = await _authenticationService.SignUp(signupRequest);
            return Ok(response);
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest loginRequest)
        {
           var response=await _authenticationService.Login(loginRequest);
            return Ok(response); 
        }
       
        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<ActionResult<RefreshTokenResponse>> Refresh([FromBody] RefreshTokenRequest refreshTokenRequest)
        {
            var response = await _authenticationService.Refresh(refreshTokenRequest);
            return Ok(response);
        }

        [HttpGet("[action]")]
        [Authorize(AuthorizationScope.Products.Get)]
        public IActionResult Validate()
        {
            return Ok();//return user
        }
    }
}
