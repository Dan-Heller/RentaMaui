using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renta.Models
{
    public class Item
    {

        public string? OwnerId { get; set; }

        public string? Id { get; set; }
        public string? Name { get; set; }

        public string? Category { get; set; }

        public float ItemRating { get; set; }

        public string RatingAsString { get => ItemRating.ToString("0.0"); }

        public int? PricePerDay { get; set; }

        public string? Description { get; set; }

        public List<string>? ImagesUrls { get; set; } = new List<string>();

        public List<DatesRange>? UnAvailableDates { get; set; } = new List<DatesRange>();

        public DateTime? UploadDate { get; set; }


        public Item() { }

        //public Item(string i_Password, string i_Email = null, int i_CellphoneNumber = 0000) //change default values eventually
        //{

        //    this.CellphoneNumber = i_CellphoneNumber;
        //   // this.Password = i_Password;
        //    this.Email = i_Email;
        //}

        //public string GetFullName()
        //{
        //    return this.FirstName + " " + this.LastName;
        //}
    }
}
