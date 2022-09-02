using Renta.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renta.Models
{
    public class Notification
    {
       
        public string? Id { get; set; }

        public ENotificationType NotificationType { get; set; }

       
        public string OtherUserId { get; set; }

        public DateTime Time { get; set; }

        public Notification(ENotificationType notificationType, string otherUserId, DateTime time)
        {
            NotificationType = notificationType;
            OtherUserId = otherUserId;
            Time = time;
        }

    }
}
