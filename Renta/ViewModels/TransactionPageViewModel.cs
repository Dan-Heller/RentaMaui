using Newtonsoft.Json;
using Renta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renta.ViewModels
{

    [QueryProperty(nameof(TransactionString), "transaction")]
    public class TransactionPageViewModel : BaseViewModel
    {
        public TransactionPageViewModel()
        {

        }

        public string DatesAsString { get; set; }

        public Transaction _transaction { get; set; }

        private string _TransactionString;

        public String TransactionString
        {
            get => _TransactionString;
            set
            {
                _TransactionString = Uri.UnescapeDataString(value ?? string.Empty);

            }
        }



        public void deserializeString()
        {
            _transaction = JsonConvert.DeserializeObject<Transaction>(_TransactionString);
            DatesAsString = _transaction.StartDate.Date.ToString("dd/MM/yyyy") + "\n-  " + _transaction.EndDate.Date.ToString("dd/MM/yyyy");
        }
    }
}
