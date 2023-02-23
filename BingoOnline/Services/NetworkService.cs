using BingoOnline.Interfaces;
using BingoOnline.Models;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Reactive.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BingoOnline.Services
{
    public class StatusError
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }

        public StatusError(bool success, int statusCode, string errorMessage)
        {
            Success = success;
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
        }
        public StatusError()
        {

        }
    }
    public record User(string username, string key);
    public record ButtonState(int number, bool pressed);

    public class NetworkService : INetworkService
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        public bool IsRegistered { get; set; }
        public User User { get; private set; }
        private HttpClient _httpClient;
        private string _baseurl;
        public NetworkService()
        {
            Logger.Info("NetworkService Has been Initialized!");
#if DEBUG
            _baseurl = "http://localhost:8080";
            Logger.Info("Current Environment: Debug");
#else
            _baseurl = "https://bingo.gurindainoponpokopiinoponpokonaanochokyumeinochosuke.xyz/";
            Logger.Info("Current Environment: Release");
#endif

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_baseurl);
            _httpClient.Timeout = new TimeSpan(0, 0, 4);
        }
        //TODO: PARSE RESP

        public async Task<StatusError> RegisterPlayer(string key, string username)
        {
            var u = new User(username, key);
            var user = JsonSerializer.Serialize(u);
            var data = new StringContent(user, Encoding.UTF8, "application/json");
            var status = new StatusError();
            var errorText = "";
            Logger.Info("User Registration in process.");
            try
            {
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("SessionKey", key);
                _httpClient.DefaultRequestHeaders.Add("Username", username);
                var resp = await _httpClient.PostAsync("/register", data);
                if (resp.IsSuccessStatusCode)
                {
                    IsRegistered = true;
                    User = u;
                    errorText = await resp.Content.ReadAsStringAsync();
                    status.Success = true;
                    status.StatusCode = (int)resp.StatusCode;
                    status.ErrorMessage = errorText;
                    Logger.Info("User Registration Success");
                    return status;

                }

                status.Success = false;
                status.StatusCode = (int)resp.StatusCode;
                errorText = await resp.Content.ReadAsStringAsync();
                status.ErrorMessage = errorText;

                Logger.Info("Something went wrong while Registering: {0}",errorText);


                return status;
            }

            catch (Exception ex)
            {
                Logger.Error(ex, "Ah Crap");
                status.Success = false;
                status.StatusCode = 999;


                //Logger.Debug("Disposing and re-creating NetworkService!");
                //For some reason it only worked like this
                //_httpClient.Dispose();
                //_httpClient = new HttpClient();
                //_httpClient.BaseAddress = new Uri(_baseurl);

                return status;
            }
            
        }

        public async Task SendBitmap(byte[] buffer)
        {
            throw new System.NotImplementedException();
        }

        public async Task SendConfig()
        {
            throw new System.NotImplementedException();
        }

        //TODO: PARSE RESP
        public async Task SendKey(int number, bool pressed)
        {
            if (IsRegistered)
            {
                var btnState = JsonSerializer.Serialize(new ButtonState(number, pressed));
                var data = new StringContent(btnState, Encoding.UTF8, "application/json");
                var resp = await _httpClient.PostAsync("/s", data);
            }
        }

        //Current Idea is to just poll this every 5s or sth
        public Task<BingoSessionInfo> GetConfig()
        {
            throw new NotImplementedException();
        }
    }
}
