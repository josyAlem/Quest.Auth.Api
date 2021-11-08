using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quest.Auth.Common.Response
{
    public class RefreshTokenResponse
    {
        public string RefreshToken { get; set; }
        public string ClientId { get; set; }
    }

   
}
