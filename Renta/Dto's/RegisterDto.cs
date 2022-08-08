using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renta.Dto_s
{
    public class RegisterDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string City { get; set; }


        public RegisterDto() { }

        public RegisterDto(string i_Email, string i_Password, string i_FirstName, string i_LastName,string i_City) 
        {
           
            this.Password = i_Password;
            this.Email = i_Email;
            this.FirstName = i_FirstName;
            this.LastName = i_LastName;
            this.City = i_City;
        }
    }
}
