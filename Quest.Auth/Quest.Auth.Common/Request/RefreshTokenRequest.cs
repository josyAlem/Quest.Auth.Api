namespace Quest.Auth.Common.Request
{
    public class RefreshTokenRequest
    {
        public string RefreshToken { get; set; }
        public string ClientId { get; set; }
    }
}
