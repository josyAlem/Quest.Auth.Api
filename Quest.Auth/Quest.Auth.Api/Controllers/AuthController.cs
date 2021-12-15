using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Quest.Auth.Api.Helpers.Auth;
using Quest.Auth.Common.Request;
using Quest.Auth.Common.Response;
using Quest.Auth.Services.Interfaces;
using System;
using System.Threading.Tasks;


namespace Quest.Auth.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly string _connString="";
        public AuthController(IAuthenticationService authenticationService,IConfiguration config)
        {
            _authenticationService = authenticationService;
            _connString = config.GetConnectionString("Default");
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
            var x = User;
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
        [HttpGet("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> CheckDb()
        {
            try
            {
               string result= ReadOrderData(_connString);


                return Ok(result);
            }
            catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }
        private  string ReadOrderData(string connectionString)
        {
            string queryString =
                "SELECT Id,Name FROM dbo.TestTable;";
            string result = "";
            using (SqlConnection connection = new SqlConnection(
                       connectionString))
            {
                SqlCommand command = new SqlCommand(
                    queryString, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result+=(String.Format("{0}, {1}",
                            reader[0], reader[1]));
                    }
                }
            }
            return result;
        }
    }
}
