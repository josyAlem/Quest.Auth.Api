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
        public SignupResponse SignUp(SignupRequest signupRequest);
        public LoginResponse Login(LoginRequest loginRequest);
        public LoginResponse Refresh(RefreshTokenRequest refreshTokenRequest);
    }
}
