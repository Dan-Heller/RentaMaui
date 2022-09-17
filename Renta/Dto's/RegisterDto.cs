using Renta.enums;

namespace Renta.Dto_s
{
    public class RegisterDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Region { get; set; }

        public string City { get; set; }

        public string FCMToken { get; set; }

        public List<ECategories>? FavoritesCategories { get; set; }

        public RegisterDto()
        {
        }

        public RegisterDto(string i_Email, string i_Password, string i_FirstName, string i_LastName, string i_City,
            string i_Region, List<ECategories> i_FavoritesCategories)
        {
            Password = i_Password;
            Email = i_Email;
            FirstName = i_FirstName;
            LastName = i_LastName;
            City = i_City;
            Region = i_Region;
            FavoritesCategories = i_FavoritesCategories;
        }
    }
}