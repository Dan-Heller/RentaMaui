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
        private List<Transaction> Transactions = new List<Transaction>();
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
           Transactions = await _transactionService.GetSeekerTransactionsByStatus(selectedStatusInTabsController);


            //if (podcastsModels == null)
            //{
            //    await Shell.Current.DisplayAlert(
            //        AppResource.Error_Title,
            //        AppResource.Error_Message,
            //        AppResource.Close);

            //    return;
            //}


            TransactionsViewModel = ConvertToViewModels(Transactions);
            UpdateTransactionsCollection(TransactionsViewModel);

        }

        private List<TransactionViewModel> ConvertToViewModels(List<Transaction> Transactions)
        {
            var viewmodels = new List<TransactionViewModel>();
            foreach (var transaction in Transactions)
            {
                var transactionVM = new TransactionViewModel(transaction);
                viewmodels.Add(transactionVM);
            }
            return viewmodels;
        }

        private void UpdateTransactionsCollection(IEnumerable<TransactionViewModel> TransactionsVM)
        {
            TransactionCollection.ReplaceRange(TransactionsVM);
        }

    }
}
