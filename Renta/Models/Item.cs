namespace Renta.Models
{
    public class Item
    {
        public string? OwnerId { get; set; }

        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Category { get; set; }

        public float ItemRating { get; set; }

        public string RatingAsString
        {
            get => ItemRating.ToString("0.0");
        }

        public int? PricePerDay { get; set; }

        public string? Description { get; set; }

        public string? Region { get; set; }

        public List<string>? ImagesUrls { get; set; } = new List<string>();

        public List<DatesRange>? UnAvailableDates { get; set; } = new List<DatesRange>();

        public DateTime? UploadDate { get; set; }

        public Item()
        {
        }
    }
}