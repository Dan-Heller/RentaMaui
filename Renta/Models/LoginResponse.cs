namespace Renta.Models
{
    public class LoginResponse
    {
        public UserLookedUp user { get; set; }
        public string Token { get; set; }
    }
}