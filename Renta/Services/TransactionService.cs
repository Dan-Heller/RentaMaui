using Renta.Models;
using Renta.enums;
using Renta.Dto_s;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace Renta.Services
{
    public  class TransactionService
    {
        UserService _userService;
        IConfiguration configuration;
        HttpClient httpclient;

        public TransactionService(IConfiguration config, UserService userService)
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };
            httpclient = new HttpClient(httpClientHandler);
            configuration = config;
            httpclient.MaxResponseContentBufferSize = 256000;
            _userService = userService;
        }


        public async Task CreateTransaction(CreateTransactionDto transactionDto)
        {
           
            string json = JsonConvert.SerializeObject(transactionDto);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
           

            await httpclient.PostAsync(new Uri(configuration.GetSection("Settings:ApiUrl").Value + "/Transactions"), content);
        }


        public async Task<List<TransactionLookedUp>> GetTransactionsByStatus(EUserType userType ,ETransactionStatus status)
        {
            var queryString = new QueryString()
            .Add("id", _userService.LoggedInUser.Id)
            .Add("type", userType.ToString())
            .Add("status", status.ToString());
            

            //string json = JsonConvert.SerializeObject(status);
            //StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;


            string UserId = _userService.LoggedInUser.Id;
            response = await httpclient.GetAsync(new Uri(configuration.GetSection("Settings:ApiUrl").Value + "/Transactions/" + queryString));

            string str = await response.Content.ReadAsStringAsync();
            var Transactions = JsonConvert.DeserializeObject<List<TransactionLookedUp>>(str);
            return Transactions;
        }

        public async Task UpdateTransaction(Transaction updatedTransaction)
        {
            string json = JsonConvert.SerializeObject(updatedTransaction);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;

            
            response = await httpclient.PutAsync(new Uri(configuration.GetSection("Settings:ApiUrl").Value + "/Transactions/" + updatedTransaction.Id), content);

            string str = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Transaction>(str);

            //if (UserUpdatedInvoker != null)
            //{
            //    UserUpdatedInvoker.Invoke();
            //}

        }
    }

   
}
