namespace Quest.Auth.Common.Request
{
    public class Auth0SignupRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Connection { get; set; }
        public string Auth0ApiToken { get; set; }
    }
}
