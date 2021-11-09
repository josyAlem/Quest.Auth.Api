using Quest.Auth.Common.Request;
using Quest.Auth.Common.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quest.Auth.Services.Interfaces
{
 public interface IAuthenticationService
    {
        Task<SignupResponse> SignUp(SignupRequest signupRequest);
        Task<LoginResponse> Login(LoginRequest loginRequest);
        Task<RefreshTokenResponse> Refresh(RefreshTokenRequest refreshTokenRequest);
    }
}
