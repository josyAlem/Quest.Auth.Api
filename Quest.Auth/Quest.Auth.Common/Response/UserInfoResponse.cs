using System;

namespace Quest.Auth.Common.Response
{
    public class UserInfoResponse
    {
    public string UserName { get; set; }
    public string Email { get; set; }
    public DateTime UpdatedOn { get; set; }
        public string[] Roles { get; set; }
    }
}
