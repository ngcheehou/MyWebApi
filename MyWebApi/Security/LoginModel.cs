namespace MyWebApi.Security
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class JwtResponse
    {
        public string Token { get; set; }
    }
}
