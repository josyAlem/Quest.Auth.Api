﻿namespace Quest.Auth.Common.Response
{
    public class LoginResponse
    {
    public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public bool IsAdmin { get; set; }
        public int ExpiresIn { get; set; }
    }
}
