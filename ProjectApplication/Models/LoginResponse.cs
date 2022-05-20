namespace ProjectApplication.Models
{
    public class LoginResponse
    {
        public string Token { get; set; }

        public LoginResponse(string token)
        {
            Token = token;
        }
    }
}
