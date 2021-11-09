using Microsoft.Extensions.Options;
using Quest.Auth.Common.Request;
using Quest.Auth.Common.Response;
using Quest.Auth.Common.Settings;
using Quest.Auth.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Quest.Auth.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly Auth0Settings _auth0Settings;
        private readonly IAuth0Service _auth0Service;

        public AuthenticationService(IAuth0Service auth0Service, IOptions<Auth0Settings> auth0Config)
        {
            _auth0Service = auth0Service;
            _auth0Settings = auth0Config.Value;
        }

        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            var auth0loginRequest = new Auth0LoginRequest
            {
                Audience = _auth0Settings.QuestAuth.Audience,
                ClientId = loginRequest.ClientId,
                GrantType = _auth0Settings.GrantTypes.PasswordRealm,
                Password = loginRequest.Password,
                Realm = _auth0Settings.QuestAuth.ConnectionRealm,
                Scope = _auth0Settings.QuestAuth.Scope,
                UserName = loginRequest.UserName
            };
            
           var authresponse=await _auth0Service.Login(auth0loginRequest);

            LoginResponse loginResponse = new LoginResponse
            {
                AccessToken = authresponse.AccessToken,
                ExpiresIn = authresponse.ExpiresIn,
                IsAdmin = false
            };

            return loginResponse;

        }

        public async Task<LoginResponse> Refresh(RefreshTokenRequest refreshTokenRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<SignupResponse> SignUp(SignupRequest signupRequest)
        {
            throw new NotImplementedException();
        }
    }
}
