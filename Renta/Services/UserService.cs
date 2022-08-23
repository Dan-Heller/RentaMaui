using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Renta.Models;
using Renta.Dto_s;
using RestSharp;
using Microsoft.AspNetCore.Http;

namespace Renta.Services
{
    public class UserService
    {
        public UserLookedUp LoggedInUser = null;

        IConfiguration configuration;
        HttpClient httpclient;
        // https://rentaapidev.azurewebsites.net
        public event Action UserUpdatedInvoker;


        public async Task UpdateUserInfo(User user)
        {
            string json = JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;

            string UserId = LoggedInUser.Id;
            response = await httpclient.PutAsync(new Uri(configuration.GetSection("Settings:ApiUrl").Value + "/Users/" + UserId), content);

            string str = await response.Content.ReadAsStringAsync();
            LoggedInUser  = JsonConvert.DeserializeObject<UserLookedUp>(str);

            if (UserUpdatedInvoker != null)
            {
                UserUpdatedInvoker.Invoke();
            }

        }

        

        public UserService(IConfiguration config)
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };
            httpclient = new HttpClient(httpClientHandler);
            configuration = config;
            httpclient.MaxResponseContentBufferSize = 256000;
        }

        public async Task RegisterUser(RegisterDto registerDto)
        {
            //var request = new RestRequest("​/Auth​/register", Method.Post).AddJsonBody(registerDto);
            //var response = await _client.PostAsync<string>(request);

            string json = JsonConvert.SerializeObject(registerDto);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
         
            response = await httpclient.PostAsync(new Uri(configuration.GetSection("Settings:ApiUrl").Value + "/Auth/register"), content);
        }

        public async Task LoginUser(LoginDto loginDto)
        {
            
            string json = JsonConvert.SerializeObject(loginDto);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;

           
            response = await httpclient.PostAsync(new Uri(configuration.GetSection("Settings:ApiUrl").Value + "/Auth/login"), content);
            string str = await  response.Content.ReadAsStringAsync();
            LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(str);

            LoggedInUser = loginResponse.user;
        }

        public async Task UpdateLoggedInUser()
        {
            //HttpResponseMessage response = null;
            //string UserId = LoggedInUser.Id;

            //try
            //{
            //    response = await httpclient.GetAsync(new Uri(configuration.GetSection("Settings:ApiUrl").Value + "/Users/" + UserId));
            //    string str = await response.Content.ReadAsStringAsync();
            //    LoggedInUser = JsonConvert.DeserializeObject<UserLookedUp>(str);
            //}
            //catch(ArgumentNullException e)
            //{
            //    var y = e;
            //    var x = response;
            //}
            LoggedInUser = await GetUserById(LoggedInUser.Id);

            //string str = await response.Content.ReadAsStringAsync();
            //LoggedInUser = JsonConvert.DeserializeObject<UserLookedUp>(str);
        }

        public async Task<UserLookedUp> GetUserById(string Id)
        {
            HttpResponseMessage response = null;
           
            response = await httpclient.GetAsync(new Uri(configuration.GetSection("Settings:ApiUrl").Value + "/Users/" + Id));
            string str = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UserLookedUp>(str);
        }

        public async Task<bool> GetIfBalanceIsValidForRent(int price, int daysCount)
        {
            QueryString queryString =
              new QueryString()
              .Add("price", price.ToString())
              .Add("daysCount", daysCount.ToString());

            HttpResponseMessage response = null;

            response = await httpclient.GetAsync(new Uri(configuration.GetSection("Settings:ApiUrl").Value + "/Users/Balance/" + LoggedInUser.Id + queryString));
            string str = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<bool>(str);
            
           
        }


    }
}