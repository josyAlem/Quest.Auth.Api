using Newtonsoft.Json;

namespace Quest.Auth.Common.Response
{
    public class Auth0PermissionResponse
    {
        [JsonProperty("permission_name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("resource_server_identifier")]
        public string SourceApp { get; set; }
    }
}
