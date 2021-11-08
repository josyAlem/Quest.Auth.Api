using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quest.Auth.Common.Response
{
    public class SignupResponse
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }

    public class Auth0SignupResponse
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Connection { get; set; }
        public string Auth0ApiToken { get; set; }
    }
}
