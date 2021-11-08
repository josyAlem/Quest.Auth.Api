using Quest.Auth.Common.Request;
using Quest.Auth.Common.Response;
using Quest.Auth.Services.Interfaces;
using System;

namespace Quest.Auth.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public LoginResponse Login(LoginRequest loginRequest)
        {
            throw new NotImplementedException();
        }

        public LoginResponse Refresh(RefreshTokenRequest refreshTokenRequest)
        {
            throw new NotImplementedException();
        }

        public SignupResponse SignUp(SignupRequest signupRequest)
        {
            throw new NotImplementedException();
        }
    }
}
