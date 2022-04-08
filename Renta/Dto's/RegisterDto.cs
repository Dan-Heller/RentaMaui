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

        public RegisterDto() { }

        public RegisterDto(string i_Email, string i_Password) 
        {
           
            this.Password = i_Password;
            this.Email = i_Email;
        }
    }
}
