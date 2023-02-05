using System;
using System.Threading.Tasks;

namespace BingoOnline.Interfaces
{
    public interface INetworkService
    {
        bool IsRegistered { get; set; }
        Task RegisterPlayer(string key, string username);
        Task SendKey(int index);
        Task SendBitmap(byte[] buffer);
        Task SendConfig();
    }
}
