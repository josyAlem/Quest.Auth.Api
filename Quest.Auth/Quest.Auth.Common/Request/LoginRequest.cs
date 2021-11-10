namespace Quest.Auth.Common.Request
{
    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ClientId { get; set; }
    }
}
