﻿//using Android.Graphics.Drawables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renta.Models
{
    public class Message
    {
       
        public string? Id { get; set; }

        
        public string? Sender { get; set; }

        /* public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ProfilePhotoUrl { get; set; } = string.Empty; */

        public string Text { get; set; }

        public DateTime Time { get; set; }

      
        public Message() { }

        public Message(string? sender, string text, DateTime time)
        {
            Sender = sender;
            Text = text;
            Time = time;
        }
    }
}
