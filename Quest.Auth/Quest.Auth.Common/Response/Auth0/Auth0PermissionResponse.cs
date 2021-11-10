using Newtonsoft.Json;

namespace Quest.Auth.Common.Response
{
    public class Auth0PermissionResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
