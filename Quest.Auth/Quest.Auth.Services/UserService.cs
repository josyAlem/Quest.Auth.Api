using Quest.Auth.Common.Request;
using Quest.Auth.Common.Response;
using Quest.Auth.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quest.Auth.Services
{
    public class UserService : IUserService
    {
        public List<RoleResponse> GetRoles(string userid)
        {
            throw new NotImplementedException();
        }

        public UserInfoResponse UserInfo(UserInfoRequest userInfoRequest)
        {
            throw new NotImplementedException();
        }
    }
}
