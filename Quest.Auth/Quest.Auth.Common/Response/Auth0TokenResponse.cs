using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quest.Auth.Common.Response
{
    public class Auth0TokenResponse
    {
    public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }

        public string TokenType { get; set; }
        public string Scope { get; set; }
    }
}
