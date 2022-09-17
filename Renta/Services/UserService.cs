using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Renta.Models;
using Renta.Dto_s;
using Microsoft.AspNetCore.Http;

namespace Renta.Services
{
    public class UserService
    {
        public UserLookedUp LoggedInUser { get; set; }
        readonly IConfiguration _configuration;
        readonly HttpClient _httpclient;

        public event Action UserUpdatedInvoker;

        public async Task UpdateUserInfo(User user)
        {
            string json = JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;

            string userId = LoggedInUser.Id;
            response = await _httpclient.PutAsync(
                new Uri(_configuration.GetSection("Settings:ApiUrl").Value + "/Users/" + userId), content);

            string str = await response.Content.ReadAsStringAsync();
            LoggedInUser = JsonConvert.DeserializeObject<UserLookedUp>(str);

            UserUpdatedInvoker?.Invoke();
        }

        public UserService(IConfiguration config)
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback =
                (message, cert, chain, errors) => true;
            _httpclient = new HttpClient(httpClientHandler);
            _configuration = config;
            _httpclient.MaxResponseContentBufferSize = 256000;
        }

        public async Task RegisterUser(RegisterDto registerDto)
        {
            registerDto.FCMToken = await SecureStorage.Default.GetAsync("FCMToken");
            System.Diagnostics.Debug.WriteLine($"recived token reg: {registerDto.FCMToken}");


            string json = JsonConvert.SerializeObject(registerDto);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            await _httpclient.PostAsync(
                new Uri(_configuration.GetSection("Settings:ApiUrl").Value + "/Auth/register"), content);
        }

        public async Task<bool> LoginUser(LoginDto loginDto)
        {
            try
            {
                loginDto.FCMToken = await SecureStorage.GetAsync("FCMToken");

                System.Diagnostics.Debug.WriteLine($"recived token login: {loginDto.FCMToken}");

                string json = JsonConvert.SerializeObject(loginDto);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpclient.PostAsync(
                    new Uri(_configuration.GetSection("Settings:ApiUrl").Value + "/Auth/login"), content);
                string str = await response.Content.ReadAsStringAsync();
                LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(str);

                LoggedInUser = loginResponse?.user;
                await SecureStorage.Default.SetAsync("AuthToken", loginResponse.Token);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task GetUserFromToken(string token)
        {
            var response = await _httpclient.GetAsync(
                new Uri(_configuration.GetSection("Settings:ApiUrl").Value + "/Auth/" + token));
            string responseAsString = await response.Content.ReadAsStringAsync();
            LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseAsString);
            LoggedInUser = loginResponse?.user;
        }

        public async Task UpdateLoggedInUser()
        {
            LoggedInUser = await GetUserById(LoggedInUser.Id);
        }

        public async Task<UserLookedUp> GetUserById(string id)
        {
            HttpResponseMessage response = null;

            response = await _httpclient.GetAsync(new Uri(_configuration.GetSection("Settings:ApiUrl").Value + "/Users/" +
                                                         id));
            string str = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UserLookedUp>(str);
        }

        public async Task<bool> GetIfBalanceIsValidForRent(int price, int daysCount)
        {
            QueryString queryString =
                new QueryString()
                    .Add("price", price.ToString())
                    .Add("daysCount", daysCount.ToString());

            var response = await _httpclient.GetAsync(new Uri(_configuration.GetSection("Settings:ApiUrl").Value +
                                                             "/Users/Balance/" + LoggedInUser.Id + queryString));
            string str = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<bool>(str);
        }
    }
}