using AutoMapper;
using Microsoft.Extensions.Options;
using Quest.Auth.Common.Request;
using Quest.Auth.Common.Response;
using Quest.Auth.Common.Settings;
using Quest.Auth.Services.Interfaces;
using System;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Quest.Auth.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly Auth0Settings _auth0Settings;
        private readonly IMapper _mapper;
        private readonly IAuth0Service _auth0Service;

        public AuthenticationService(IAuth0Service auth0Service, IOptions<Auth0Settings> auth0Config,IMapper mapper)
        {
            _auth0Service = auth0Service;
            _auth0Settings = auth0Config.Value;
            _mapper = mapper;
        }

        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            Auth0LoginRequest auth0loginRequest = _mapper.Map<Auth0LoginRequest>(loginRequest);
            auth0loginRequest.Audience = _auth0Settings.QuestAuth.Audience;
            auth0loginRequest.GrantType = _auth0Settings.GrantTypes.PasswordRealm;
            auth0loginRequest.Realm = _auth0Settings.QuestAuth.ConnectionRealm;
            auth0loginRequest.Scope = _auth0Settings.QuestAuth.Scope;
            var authresponse =await _auth0Service.Login(auth0loginRequest);

            LoginResponse loginResponse = _mapper.Map<LoginResponse>(authresponse);
            loginResponse.IsAdmin = false;//TODO; get from Roles
           

            return loginResponse;

        }

        public async Task<RefreshTokenResponse> Refresh(RefreshTokenRequest refreshTokenRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<SignupResponse> SignUp(SignupRequest signupRequest)
        {
            throw new NotImplementedException();
        }
    }
}
