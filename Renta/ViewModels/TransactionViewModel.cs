using Renta.Services;
using Renta.enums;
using Renta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Renta.Views;

namespace Renta.ViewModels
{
    public class TransactionViewModel
    {
        public Transaction Transaction { get; set; }
        public Item Item { get; set; }

        private TransactionService _transactionService;

        private UserService _userService;
        public bool NeedApproveIconOnOwner { get; set; }

        public bool NeedApproveIconOnSeeker { get; set; }

        public bool NeedCancelIcon { get; set; }

        public string? Id { get => Transaction?.Id; }


        public string? ItemOwner { get => Transaction?.ItemOwner; }


        public string? ItemSeeker { get => Transaction?.ItemSeeker; }


        public string? ItemId { get => Transaction?.ItemId; }

        public DateTime? StartDate { get => Transaction?.StartDate; }

        public DateTime? EndDate { get => Transaction?.EndDate; }

        public DateTime? CreatedAt { get => Transaction?.CreatedAt; }


        public ETransactionStatus Status { get => Transaction.Status; }

        public delegate Task TransactionStatusChangedDelegate();
        public TransactionStatusChangedDelegate TransactionStatusChanged;

        public string DatesAsString { get; set; }

        //private ItemService _itemService;
        public string FirstImageUrl { get { return Item.ImagesUrls[0]; } }

        public TransactionViewModel(TransactionLookedUp transaction, TransactionService transactionService, UserService userService)
        {
            Transaction = transaction;
            _userService = userService;
            Item = transaction.GetItem();
            _transactionService = transactionService;
            // ItemVM = new ItemViewModel(item);
            //Task.Run(async () => await GetItem());
            DatesAsString = StartDate.Value.Date.ToString("dd/MM/yyyy") + "\n-  " + EndDate.Value.Date.ToString("dd/MM/yyyy");


            if(transaction.Status == ETransactionStatus.Approved || transaction.Status == ETransactionStatus.Active || transaction.Status == ETransactionStatus.Archived || transaction.Status == ETransactionStatus.Canceled)
            {
                NeedCancelIcon = false;
            }
            else
            {
                NeedCancelIcon = true;
            }

            //NeedApproveIconOnOwner = Status == ETransactionStatus.Pending || (Status == ETransactionStatus.Approved  && CheckDateRange() && (!transaction.OwnerAcceptedActivation));
            NeedApproveIconOnOwner = checkedForOwnerApproveIcon(transaction);
            NeedApproveIconOnSeeker = checkedForSeekerApproveIcon(transaction);

           // NeedApproveIconOnSeeker = Status == ETransactionStatus.Approved && CheckDateRange() && !transaction.SeekerAcceptedActivation;
            
        }

        private bool checkedForOwnerApproveIcon(TransactionLookedUp transaction)
        {
            return Status == ETransactionStatus.Pending ||
                   (Status == ETransactionStatus.Approved && CheckDateRange() && (!transaction.OwnerAcceptedActivation)) ||
                   (Status == ETransactionStatus.Active && CheckLastRentingDayArrive() && (!transaction.OwnerAcceptedCompletion));
        }

        private bool checkedForSeekerApproveIcon(TransactionLookedUp transaction)
        {
            return Status == ETransactionStatus.Approved && CheckDateRange() && !transaction.SeekerAcceptedActivation ||
                    Status == ETransactionStatus.Active && CheckLastRentingDayArrive() && !transaction.SeekerAcceptedCompletion;
        }



        private bool CheckLastRentingDayArrive()
        {

            DateTime currentDate = DateTime.Now.Date;
            DateTime start = this.StartDate.Value.Date;
            DateTime end = this.EndDate.Value.Date;

            return (currentDate >= end);
        }

        private bool CheckDateRange()
        {
            DateTime currentDate = DateTime.Now.Date;
            DateTime start = this.StartDate.Value.Date;
            DateTime end = this.EndDate.Value.Date;

            //if (DateTime.Compare(currentDate, start) >= 0 && DateTime.Compare(currentDate, end) <= 0)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}

            if (currentDate >= start && currentDate <= end)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public Command Approve_Clicked
        => new Command(async () => await HandleApproveRequest());

        public Command Cancel_Clicked
        => new Command(async () => await HandleCancelRequest());

        private async Task HandleApproveRequest()
        {

            if (Status == ETransactionStatus.Approved || Status == ETransactionStatus.Active)
            {
                    var jsonStr = JsonConvert.SerializeObject(Transaction);
                    await Shell.Current.GoToAsync($"{nameof(ActivateTransactionPage)}?transaction={jsonStr}");
            }

            if (Status == ETransactionStatus.Pending)
            {
                Transaction.Status = ETransactionStatus.Approved;
                await _transactionService.UpdateTransaction(Transaction); //update in database
                TransactionStatusChanged?.Invoke(); // tells the page to update collection.
            }
            
        }

        private async Task HandleCancelRequest()
        {
            Transaction.Status = ETransactionStatus.Canceled;
            await _transactionService.UpdateTransaction(Transaction); //update in database
            TransactionStatusChanged?.Invoke(); // tells the page to update collection.
        }

        public Command OwnerProfileLink_Tapped
      => new Command(async () => await Shell.Current.GoToAsync($"{nameof(OtherUserProfilePage)}?OwnerId={ItemOwner}"));

        public Command SeekerProfileLink_Tapped
=> new Command(async () => await Shell.Current.GoToAsync($"{nameof(OtherUserProfilePage)}?OwnerId={ItemSeeker}"));

        public Command ItemPhotoTapped
=> new Command(async () => await GotoItemPage());

        private async Task GotoItemPage()
        {
            var jsonStr = JsonConvert.SerializeObject(Item);
            await Shell.Current.GoToAsync($"{nameof(ItemPage)}?item={jsonStr}");
        }


        public Command MyItemPhotoTapped
  => new Command(async () => await GotoMyItemPage());

        private async Task GotoMyItemPage()
        {
            var jsonStr = JsonConvert.SerializeObject(Item);
            await Shell.Current.GoToAsync($"{nameof(MyItemPage)}?item={jsonStr}");
        }

        public Command GoToTransactionPage
=> new Command(async () => await NavigateToTransactionPage());

        private async Task NavigateToTransactionPage()
        {
            var jsonStr = JsonConvert.SerializeObject(Transaction);
            await Shell.Current.GoToAsync($"{nameof(TransactionPage)}?transaction={jsonStr}");
            //await Shell.Current.GoToAsync($"{nameof(TransactionPage)}");
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
