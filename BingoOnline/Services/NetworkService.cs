using BingoOnline.Interfaces;
using ReactiveUI;
using System;
using System.Net.Http;
using System.Reactive.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BingoOnline.Services
{
    public record User(string username, string key);

    public class NetworkService : INetworkService
    {
        public bool IsRegistered { get; set; }
        
        private HttpClient _httpClient;
        private string _baseurl;
        public NetworkService()
        {

#if DEBUG
            _baseurl = "http://localhost:8080";
#else
            _baseurl = "https://bingo.gurindainoponpokopiinoponpokonaanochokyumeinochosuke.xyz/";
#endif
            
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_baseurl);
        }

        public async Task RegisterPlayer(string key, string username)
        {
            var user = JsonSerializer.Serialize(new User(username, key));
            var data = new StringContent(user, Encoding.UTF8, "application/json");
            var resp = await _httpClient.PostAsync("/register", data);
            IsRegistered = true;
        }

        public async Task SendBitmap(byte[] buffer)
        {
            throw new System.NotImplementedException();
        }

        public async Task SendConfig()
        {
            throw new System.NotImplementedException();
        }

        public async Task SendKey(int index)
        {
            throw new System.NotImplementedException();
        }

    }
}
