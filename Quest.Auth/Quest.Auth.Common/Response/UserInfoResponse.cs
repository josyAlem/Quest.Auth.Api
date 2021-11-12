using System;
using System.Collections.Generic;

namespace Quest.Auth.Common.Response
{
    public class UserInfoResponse
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime UpdatedOn { get; set; }
        public List<string> Permissions { get; set; }
        public List<string> Roles { get; set; }
    }
}
