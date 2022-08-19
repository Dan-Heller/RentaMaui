using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renta.Models
{
    public class User
    {
        public int? CellphoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePhotoUrl { get; set; }

        public float UserRating { get; set; }

        public string RatingAsString { get => UserRating.ToString("0.0"); }

        public int Coins { get; set; }
        public string? City { get; set; }

        public string? Region { get; set; }

        public string Id { get; set; }
        //public string Password { get; set; }
        public string Email { get; set; }

        public  List<string> Transactions { get; set; }

        public List<string> LikedItems { get; set; }

        public List<string>? Chats { get; set; }

        public List<Chat>? PopulatedChats { get; set; }

        public List<string> Items { get; set; } = new List<string>();

        public User() { }

        public User(string i_Password, string i_Email = null, int i_CellphoneNumber = 0000) //change default values eventually
        {
            
            this.CellphoneNumber = i_CellphoneNumber;
           // this.Password = i_Password;
            this.Email = i_Email;
        }

        public string GetFullName()
        {
            return this.FirstName + " " + this.LastName;
        }
    }


}
