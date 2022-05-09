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


        public async Task<List<Transaction>> GetSeekerTransactionsByStatus(ETransactionStatus status)
        {

            string json = JsonConvert.SerializeObject(status);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;

           
            string UserId = _userService.LoggedInUser.Id;
            response = await httpclient.GetAsync(new Uri(configuration.GetSection("Settings:ApiUrl").Value + "/Transactions/seeker/" + UserId + content));

            string str = await response.Content.ReadAsStringAsync();
            var items = JsonConvert.DeserializeObject<List<Transaction>>(str);

            return items;
        }
    }

   
}
