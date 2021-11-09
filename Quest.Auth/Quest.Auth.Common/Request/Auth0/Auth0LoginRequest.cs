namespace Quest.Auth.Common.Request
{
    public class Auth0LoginRequest
    {
      public string Realm { get; set; }
        public string Audience { get; set; }
        public string ClientId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public string GrantType { get; set; }
        public string Scope { get; set; }
    }
}
