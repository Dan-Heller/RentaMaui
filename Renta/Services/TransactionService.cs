using Renta.Models;
using Renta.enums;
using Renta.Dto_s;
using Microsoft.Extensions.Configuration;
using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace Renta.Services
{
    public class TransactionService
    {
        readonly UserService _userService;
        readonly IConfiguration _configuration;
        readonly HttpClient _httpclient;

        public TransactionService(IConfiguration config, UserService userService)
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback =
                (message, cert, chain, errors) => { return true; };
            _httpclient = new HttpClient(httpClientHandler);
            _configuration = config;
            _httpclient.MaxResponseContentBufferSize = 256000;
            _userService = userService;
        }

        public async Task CreateTransaction(CreateTransactionDto transactionDto)
        {
            transactionDto.StartDate = transactionDto.StartDate.Value.AddHours(2);
            transactionDto.EndDate = transactionDto.EndDate.Value.AddHours(2);
            string json = JsonConvert.SerializeObject(transactionDto);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            System.Diagnostics.Debug.WriteLine(content);

            await _httpclient.PostAsync(new Uri(_configuration.GetSection("Settings:ApiUrl").Value + "/Transactions"),
                content);
        }


        public async Task<List<TransactionLookedUp>> GetTransactionsByStatus(EUserType userType,
            ETransactionStatus status)
        {
            var queryString = new QueryString()
                .Add("id", _userService.LoggedInUser.Id)
                .Add("type", userType.ToString())
                .Add("status", status.ToString());

            HttpResponseMessage response = null;
            string userId = _userService.LoggedInUser.Id;
            response = await _httpclient.GetAsync(new Uri(_configuration.GetSection("Settings:ApiUrl").Value +
                                                         "/Transactions/" + queryString));
            string str = await response.Content.ReadAsStringAsync();
            var transactions = JsonConvert.DeserializeObject<List<TransactionLookedUp>>(str);
            return transactions;
        }

        public async Task UpdateTransaction(Transaction updatedTransaction)
        {
            string json = JsonConvert.SerializeObject(updatedTransaction);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            response = await _httpclient.PutAsync(
                new Uri(_configuration.GetSection("Settings:ApiUrl").Value + "/Transactions/" + updatedTransaction.Id),
                content);

            string str = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Transaction>(str);
        }
    }
}