using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quest.Auth.Common.Response
{
    public class RoleResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class Auth0RoleResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
