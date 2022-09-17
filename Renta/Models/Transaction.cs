using Renta.enums;

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
        public List<string>? SeekerImagesBefore { get; set; }
        public List<string>? OwnerImagesBefore { get; set; }
        public List<string>? SeekerImagesAfter { get; set; }
        public List<string>? OwnerImagesAfter { get; set; }
        public bool OwnerAcceptedActivation = false;
        public bool SeekerAcceptedActivation = false;
        public bool OwnerAcceptedCompletion = false;
        public bool SeekerAcceptedCompletion = false;
        public bool OwnerReviewedSeeker = false;
        public bool SeekerReviewedOwner = false;
        public bool SeekerReviewedItem = false;

        public Transaction()
        {
        }
    }

    public class TransactionLookedUp : Transaction
    {
        public List<Item>? items { get; set; }

        public Item GetItem()
        {
            if (items != null && items.Count > 0)
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