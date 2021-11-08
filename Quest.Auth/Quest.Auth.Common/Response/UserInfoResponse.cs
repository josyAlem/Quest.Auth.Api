using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quest.Auth.Common.Response
{
    public class UserInfoResponse
    {
    public string UserName { get; set; }
    public string Email { get; set; }
    public DateTime UpdatedOn { get; set; }
        public string[] Roles { get; set; }
    }

    public class Auth0UserInfoResponse
    {
        public string ClientId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string[] Roles { get; set; }
    }
}
