namespace Renta.Dto_s
{
    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public string FCMToken { get; set; }

        public LoginDto()
        {
        }

        public LoginDto(string i_Email, string i_Password)
        {
            Password = i_Password;
            Email = i_Email;
        }
    }
}