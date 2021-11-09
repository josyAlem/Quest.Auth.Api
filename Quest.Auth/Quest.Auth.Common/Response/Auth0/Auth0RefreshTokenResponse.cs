﻿using Newtonsoft.Json;

namespace Quest.Auth.Common.Response
{
    public class Auth0RefreshTokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
        [JsonProperty("id_token")]
        public string IdToken { get; set; }
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        [JsonProperty("scope")]
        public string Scope { get; set; }
    }
   
}
