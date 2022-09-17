using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renta.Models
{
    public class ItemReview
    {
        public string? Id { get; set; }
        public string? OwnerId { get; set; }
        public string? SeekerId { get; set; }
        public string? SeekerName { get; set; }
        public float ItemRating { get; set; }
        public string? TransactionId { get; set; }
        public string? ItemId { get; set; }
        public string? Review { get; set; }
        public float Rating { get => ItemRating; }
        public DateTime? DateOfReview { get; set; }
        public ItemReview(string ownerId, string seekerId, int itemRating, string transactionId, string itemId, string review, DateTime dateTime)
        {
            OwnerId = ownerId;
            SeekerId = seekerId;
            ItemId = itemId;
            Review = review;
            DateOfReview = dateTime;
            TransactionId = transactionId;
            ItemRating = itemRating;
        }

        public ItemReview()
        {

        }

        //ctor for creation of the review 
        public ItemReview(string ownerId, string seekerId, int itemRating, string transactionId, string itemId, string review)
        {
            OwnerId = ownerId;
            SeekerId = seekerId;
            ItemId = itemId;
            Review = review;
            TransactionId = transactionId;
            ItemRating = itemRating;
        }



    }
}
