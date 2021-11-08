using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quest.Auth.Common.Request
{
    public class UserInfoRequest
    {
    public string AccessToken { get; set; }
    }

    public class Auth0UserInfoRequest
    {
        public string AccessToken { get; set; }

    }
}
