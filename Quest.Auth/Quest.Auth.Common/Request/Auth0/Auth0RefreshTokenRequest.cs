namespace Quest.Auth.Common.Request
{
    public class Auth0RefreshTokenRequest
    {
        public string RefreshToken { get; set; }
        public string ClientId { get; set; }
        public string GrantType { get; set; }
    }
}
