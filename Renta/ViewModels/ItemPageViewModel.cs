﻿using Newtonsoft.Json;
using Renta.Dto_s;
using Renta.Models;
using Renta.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renta.ViewModels
{
    [QueryProperty(nameof(ItemString), "item")]
    public  class ItemPageViewModel : BaseViewModel
    {
        public bool ItemLiked;
        public ItemViewModel _ItemViewModel { get; set; }

        private UserService _userService { get; set; }
        private TransactionService _transactionService { get; set; }


        private string _ItemString;
        public String ItemString
        {
            get => _ItemString;
            set
            {
                _ItemString = Uri.UnescapeDataString(value ?? string.Empty);

            }
        }


        public void deserializeString()
        {
            var Item = JsonConvert.DeserializeObject<Item>(_ItemString);
            convertItemToViewModel(Item);
        }

        private void convertItemToViewModel(Item item)
        {
            _ItemViewModel = new ItemViewModel(item);
            OnPropertyChanged(nameof(_ItemViewModel));
        }


        public ItemPageViewModel(UserService userService, TransactionService transactionService)
        {
            ItemLiked = false; //should be set according to the user liked items in data base/
            _userService = userService;
            _transactionService = transactionService;
        }

       
        public Command ProfileLink_Tapped
        => new Command(async () => await Shell.Current.GoToAsync($"{nameof(OtherUserProfilePage)}?OwnerId={_ItemViewModel.OwnerId}"));

        public Command SendRequestButton_Clicked
       => new Command(async () => await sendItemRequest());

        private async Task sendItemRequest()
        {
            var transactionDto = createTransaction();
            await _transactionService.CreateTransaction(transactionDto);
        }

        private CreateTransactionDto createTransaction()
        {
            CreateTransactionDto transaction = new CreateTransactionDto();
            transaction.ItemSeeker = _userService.LoggedInUser.Id;
            transaction.ItemOwner = _ItemViewModel.OwnerId;
            transaction.ItemId = _ItemViewModel.Id;
            return transaction;
        }



    }
}
