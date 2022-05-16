﻿using Renta.Services;
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
        public Item Item { get; set; }

        private TransactionService _transactionService;
       

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

        public TransactionViewModel(TransactionLookedUp transaction, TransactionService transactionService)
        {
            Transaction = transaction;
            Item = transaction.GetItem();
            _transactionService = transactionService;
            // ItemVM = new ItemViewModel(item);
            //Task.Run(async () => await GetItem());
            DatesAsString = StartDate.Value.Date.ToString("dd/MM/yyyy") + "-  " + EndDate.Value.Date.ToString("dd/MM/yyyy");
        }

        public Command Approve_Clicked
        => new Command(async () => await HandleApproveRequest());

        public Command Cancel_Clicked
        => new Command(async () => await HandleCancelRequest());

        private async Task HandleApproveRequest()
        {
            if(Status == ETransactionStatus.Pending)
            {
                Transaction.Status = ETransactionStatus.Approved;
            }
            await _transactionService.UpdateTransaction(Transaction); //update in database

            TransactionStatusChanged?.Invoke(); // tells the page to update collection.

            

        }

        private async Task HandleCancelRequest()
        {
           Transaction.Status = ETransactionStatus.Canceled;
            await _transactionService.UpdateTransaction(Transaction); //update in database
            TransactionStatusChanged?.Invoke(); // tells the page to update collection.
        }

        // create async factory pattern 
        //private TransactionViewModel(Transaction transaction, Item item)
        //{
        //    Transaction = transaction;
        //    ItemVM = new ItemViewModel(item);
        //}



        //public static async Task<TransactionViewModel> CreateTransactionViewModelAsync(Transaction transaction, ItemService itemService)
        //{
        //    var item = await itemService.GetItemById(transaction.ItemId); 
        //    return new TransactionViewModel(transaction, item);
        //}





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
