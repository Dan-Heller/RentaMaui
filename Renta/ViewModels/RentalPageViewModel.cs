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
    public class RentalPageViewModel
    {
        private List<TransactionLookedUp> Transactions = new List<TransactionLookedUp>();
        public ObservableRangeCollection<TransactionViewModel> TransactionCollection { get; private set; } = new ObservableRangeCollection<TransactionViewModel>();

        private List<TransactionViewModel> TransactionsViewModel;

        private ItemService _itemService;

        private UserService _userService;

        public  ETransactionStatus selectedStatusInTabsController = ETransactionStatus.Pending;

        private TransactionService _transactionService;

        public RentalPageViewModel(TransactionService transactionService , UserService userService)
        {
            _transactionService = transactionService;
            _userService = userService;
        }




        public async Task FetchTransactionByStatus()
        {
           Transactions = await _transactionService.GetTransactionsByStatus(EUserType.Seeker,selectedStatusInTabsController);
            if (selectedStatusInTabsController == ETransactionStatus.Archived)
            {
                Transactions.AddRange(await _transactionService.GetTransactionsByStatus(EUserType.Seeker, ETransactionStatus.Canceled));
            }


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


        private List<TransactionViewModel> ConvertToViewModels(List<TransactionLookedUp> Transactions)
        {
            var viewmodels = new List<TransactionViewModel>();
            
            foreach (var transaction in Transactions)
            {
                
                var transactionVM = new TransactionViewModel(transaction, _transactionService, _userService);
                transactionVM.TransactionStatusChanged += FetchTransactionByStatus;
               
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
