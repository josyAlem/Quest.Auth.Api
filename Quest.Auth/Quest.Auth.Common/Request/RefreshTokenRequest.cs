﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quest.Auth.Common.Request
{
    public class RefreshTokenRequest
    {
        public string RefreshToken { get; set; }
        public string ClientId { get; set; }
    }
}