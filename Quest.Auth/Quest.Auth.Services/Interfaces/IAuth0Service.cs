using Quest.Auth.Common.Request;
using Quest.Auth.Common.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quest.Auth.Services.Interfaces
{
 public   interface IAuth0Service
    {
        Task<Auth0LoginResponse> Login(Auth0LoginRequest loginRequest);
    }
}
