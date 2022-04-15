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
        
        //public string Password { get; set; }
        public string Email { get; set; }
        
        public User() { }

        public User(string i_Password, string i_Email = null, int i_CellphoneNumber = 0000) //change default values eventually
        {
            
            this.CellphoneNumber = i_CellphoneNumber;
           // this.Password = i_Password;
            this.Email = i_Email;
        }
    }
}
