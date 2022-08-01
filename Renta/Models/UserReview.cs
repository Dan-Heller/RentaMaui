using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renta.Models
{
    public class UserReview
    {
        public string? Id { get; set; }

        
        public string? RevieweeId { get; set; }

        public string? ReviewerId { get; set; }

        public float UserRating { get; set; }

        public string? TransactionId { get; set; }

        public string? ItemId { get; set; }

        public string? Review { get ;   set; }

        

        public DateTime? DateOfReview { get; set; }

        public float Rating { get => UserRating; }

        public UserReview()
        {

        }

    

        public UserReview(string revieweeId, string reviewerId, int userRating, string transactionId, string itemId, string review)
        {
            RevieweeId = revieweeId;
            ReviewerId = reviewerId;
            ItemId = itemId;
            Review = review;
            TransactionId = transactionId;
            UserRating = userRating;
        }

        public UserReview(string revieweeId, string reviewerId, int userRating, string transactionId, string itemId, string review, DateTime datetime)
        {
            RevieweeId = revieweeId;
            ReviewerId = reviewerId;
            ItemId = itemId;
            Review = review;
            DateOfReview = datetime;
            TransactionId = transactionId;
            UserRating = userRating;
        }


    }
}
