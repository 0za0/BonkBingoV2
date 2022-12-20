using BingoOnline.Interfaces;
using System.Net.Http;

namespace BingoOnline.Services
{
    public class NetworkService : INetworkService
    {
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
        }

        public void RegisterPlayer(string key, string username)
        {
            throw new System.NotImplementedException();
        }

        public void SendBitmap(byte[] buffer)
        {
            throw new System.NotImplementedException();
        }

        public void SendConfig()
        {
            throw new System.NotImplementedException();
        }

        public void SendKey(int index)
        {
            throw new System.NotImplementedException();
        }

    }
}
