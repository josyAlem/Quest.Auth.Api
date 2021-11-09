using Newtonsoft.Json;
using System;

namespace Quest.Auth.Common.Response
{
    public class Auth0UserInfoResponse
    {
        [JsonProperty("name")]
        public string UserName { get; set; }
        [JsonProperty("sub")]
        public string UserId { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedOn { get; set; }
        public string[] Roles { get; set; }
    }
}
