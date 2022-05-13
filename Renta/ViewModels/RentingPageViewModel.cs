using Renta.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmHelpers;
using Renta.Models;
using Renta.Services;

namespace Renta.ViewModels
{
    public class RentingPageViewModel
    {
        private List<TransactionLookedUp> Transactions = new List<TransactionLookedUp>();
        public ObservableRangeCollection<TransactionViewModel> TransactionCollection { get; private set; } = new ObservableRangeCollection<TransactionViewModel>();

        private List<TransactionViewModel> TransactionsViewModel;

        private ItemService _itemService;

        

        public  ETransactionStatus selectedStatusInTabsController = ETransactionStatus.Pending;

        private TransactionService _transactionService;

        public RentingPageViewModel(TransactionService transactionService)
        {
            _transactionService = transactionService;
           
        }



        //internal async Task InitializeAsync()
        //{
        //    await FetchAsync();
        //}

        public async Task FetchTransactionByStatus()
        {
           Transactions = await _transactionService.GetTransactionsByStatus(EUserType.Owner,selectedStatusInTabsController);
            

            //if (podcastsModels == null)
            //{
            //    await Shell.Current.DisplayAlert(
            //        AppResource.Error_Title,
            //        AppResource.Error_Message,
            //        AppResource.Close);

            //    return;
            //}


            TransactionsViewModel =  ConvertToViewModels(Transactions);
            UpdateTransactionsCollection(TransactionsViewModel);

        }

        //private  async Task<List<TransactionViewModel>> ConvertToViewModels(List<Transaction> Transactions)
        //{
        //    var viewmodels = new List<TransactionViewModel>();
        //    foreach (var transaction in Transactions)
        //    {
        //        var item = await  _itemService.GetItemById(transaction.ItemId);
        //        var transactionVM = new TransactionViewModel(transaction, item);
        //        //var transactionVM = TransactionViewModel.CreateTransactionViewModelAsync(transaction, _itemService);
        //        viewmodels.Add(transactionVM);
        //    }
        //    //await Task.WhenAll(viewmodels);
        //    return viewmodels;
        //}

        private List<TransactionViewModel> ConvertToViewModels(List<TransactionLookedUp> Transactions)
        {
            var viewmodels = new List<TransactionViewModel>();
            
            foreach (var transaction in Transactions)
            {
                
                var transactionVM = new TransactionViewModel(transaction, _transactionService);
                transactionVM.TransactionStatusChanged += FetchTransactionByStatus;
                //var transactionVM = TransactionViewModel.CreateTransactionViewModelAsync(transaction, _itemService);
                viewmodels.Add(transactionVM);
            }
            //await Task.WhenAll(viewmodels);
            return viewmodels;
        }



        private void UpdateTransactionsCollection(IEnumerable<TransactionViewModel> TransactionsVM)
        {
            TransactionCollection.ReplaceRange(TransactionsVM);
        }

    }
}
