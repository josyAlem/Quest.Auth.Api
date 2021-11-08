using Quest.Auth.Common.Request;
using Quest.Auth.Common.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quest.Auth.Services.Interfaces
{
   public  interface IUserService
    {
        public UserInfoResponse UserInfo(UserInfoRequest userInfoRequest);
        public List<RoleResponse> GetRoles(string  userid);
    }
}
