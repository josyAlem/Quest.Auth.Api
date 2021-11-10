using Quest.Auth.Common.Request;
using Quest.Auth.Common.Response;
using System.Threading.Tasks;

namespace Quest.Auth.Services.Interfaces
{
    public   interface IAuth0Service
    {
        Task<Auth0LoginResponse> Login(Auth0LoginRequest loginRequest);
        Task<Auth0SignupResponse> SignUp(Auth0SignupRequest signupRequest);
        Task<Auth0RefreshTokenResponse> Refresh(Auth0RefreshTokenRequest refreshTokenRequest);
        Task<Auth0UserInfoResponse> GetUserInfo(Auth0UserInfoRequest auth0UserInfoRequest);
    }
}
