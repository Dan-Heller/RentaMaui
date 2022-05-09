using Renta.enums;
using Renta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renta.ViewModels
{
    public class TransactionViewModel
    {
        public Transaction Transaction { get; set; }


       

    public string? Id { get => Transaction?.Id; }


        public string? ItemOwner { get => Transaction?.ItemOwner; }


        public string? ItemSeeker { get => Transaction?.ItemSeeker; }


        public string? ItemId { get => Transaction?.ItemId; }

        public DateTime? StartDate { get => Transaction?.StartDate; }

        public DateTime? EndDate { get => Transaction?.EndDate; }

        public DateTime? CreatedAt { get => Transaction?.CreatedAt; }


        public ETransactionStatus Status { get => Transaction.Status; }




        public TransactionViewModel(Transaction transaction)
        {
            Transaction = transaction;
        }



    //    public Command GoToMyItemPage
    //  => new Command(async () => await MyItemCard_Tapped());

    //    private async Task MyItemCard_Tapped()
    //    {
    //        var jsonStr = JsonConvert.SerializeObject(Item);
    //        await Shell.Current.GoToAsync($"{nameof(MyItemPage)}?item={jsonStr}");
    //    }


    //    public Command GoToItemPage
    //=> new Command(async () => await ItemCard_Tapped());

    //    private async Task ItemCard_Tapped()
    //    {
    //        var jsonStr = JsonConvert.SerializeObject(Item);
    //        await Shell.Current.GoToAsync($"{nameof(ItemPage)}?item={jsonStr}");
    //    }


    }
}
