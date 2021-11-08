using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quest.Auth.Common.Response
{
    public class LoginResponse
    {
    public string AccessToken { get; set; }
        public bool IsAdmin { get; set; }
        public int ExpiresIn { get; set; }
    }

    public class Auth0LoginResponse
    {
    public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string IdToken { get; set; }
        public int ExpiresIn { get; set; }

        public string TokenType { get; set; }
        public string Scope { get; set; }
    }
}
