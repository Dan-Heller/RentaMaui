using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renta.Models
{
    public class Chat
    {
        public string? Id { get; set; }

        public string UserA { get; set; }

        public string UserB { get; set; }

        public List<Message> Messages { get; set; } = new List<Message>();

        public Chat()
        {
        }

        public Chat(string userA, string userB)
        {
            UserA = userA;
            UserB = userB;
        }
    }
}