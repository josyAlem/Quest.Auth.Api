using Quest.Auth.Common.Request;
using Quest.Auth.Common.Response;
using System.Collections.Generic;

namespace Quest.Auth.Services.Interfaces
{
    public  interface IUserService
    {
        public UserInfoResponse UserInfo(UserInfoRequest userInfoRequest);
        public List<RoleResponse> GetRoles(string  userid);
    }
}
