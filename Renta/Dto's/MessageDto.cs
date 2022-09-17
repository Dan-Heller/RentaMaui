using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renta.Models
{
    public class MessageDto
    {
        public string sender { get; set; }
        public string receiver { get; set; }
        public string message { get; set; }
        public string chatId { get; set; }
        public DateTime createdAt { get; set; }

        public MessageDto(string sender, string receiver, string message, string chatId, DateTime createdAt)
        {
            this.sender = sender;
            this.receiver = receiver;
            this.message = message;
            this.chatId = chatId;
            this.createdAt = createdAt;
        }
    }
}