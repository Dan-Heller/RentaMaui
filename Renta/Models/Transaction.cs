using Renta.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renta.Models
{
    public class Transaction
    {
        public string? Id { get; set; }

       
        public string? ItemOwner { get; set; }

        
        public string? ItemSeeker { get; set; }

       
        public string? ItemId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime? CreatedAt { get; set; } = new DateTime();

        public ETransactionStatus Status { get; set; } = ETransactionStatus.Pending;

        public List<string>? SeekerImages { get; set; }

        public List<string>? OwnerImages { get; set; }

        public bool OwnerAcceptedActivation = false;

        public bool SeekerAcceptedActivation = false;

        public Transaction()
        {

        }

    }

    public class TransactionLookedUp : Transaction
    {
        public List<Item>? items { get; set; }

        public Item GetItem()
        {
            if(items != null && items.Count > 0)
            {
                return items[0];
            }
            else
            {
                return null;
            }
            
        }

    }
}
