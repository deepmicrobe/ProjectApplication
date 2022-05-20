namespace ProjectApplication.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime Creation { get; set; }
        public DateTime LastLoggedIn { get; set; }
    }
}
