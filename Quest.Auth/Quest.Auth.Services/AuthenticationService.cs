using AutoMapper;
using Microsoft.Extensions.Options;
using Quest.Auth.Common.Request;
using Quest.Auth.Common.Response;
using Quest.Auth.Common.Settings;
using Quest.Auth.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

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

            var auth0UserinfoResponse =await _auth0Service.GetUserInfo(new Auth0UserInfoRequest { AccessToken= authresponse .AccessToken});
            UserInfoResponse userinfoResponse = _mapper.Map<UserInfoResponse>(auth0UserinfoResponse);
            LoginResponse loginResponse = _mapper.Map<LoginResponse>(authresponse);

            loginResponse.IsAdmin =userinfoResponse.Roles.Contains("admin")?true: false;//TODO; get from Roles
            loginResponse.Permissions = userinfoResponse.Permissions;

            return loginResponse;

        }

        public async Task<RefreshTokenResponse> Refresh(RefreshTokenRequest refreshTokenRequest)
        {
            Auth0RefreshTokenRequest auth0refreshTokenRequest = _mapper.Map<Auth0RefreshTokenRequest>(refreshTokenRequest);
            auth0refreshTokenRequest.GrantType = _auth0Settings.GrantTypes.Refresh;
            var authresponse = await _auth0Service.Refresh(auth0refreshTokenRequest);

            RefreshTokenResponse refreshResponse = _mapper.Map<RefreshTokenResponse>(authresponse);

            return refreshResponse;
        }

        public async Task<SignupResponse> SignUp(SignupRequest signupRequest)
        {
            Auth0SignupRequest auth0signupRequest = _mapper.Map<Auth0SignupRequest>(signupRequest);
            auth0signupRequest.Connection = _auth0Settings.QuestAuth.ConnectionRealm;
            var authresponse = await _auth0Service.SignUp(auth0signupRequest);

            SignupResponse loginResponse = _mapper.Map<SignupResponse>(authresponse);

            return loginResponse;
        }
    }
}
