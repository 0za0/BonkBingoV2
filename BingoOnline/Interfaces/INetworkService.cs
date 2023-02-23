using BingoOnline.Models;
using BingoOnline.Services;
using System;
using System.Threading.Tasks;

namespace BingoOnline.Interfaces
{
    public interface INetworkService
    {
        bool IsRegistered { get; set; }
        Task<StatusError> RegisterPlayer(string key, string username);
        Task SendKey(int number, bool pressed);
        Task SendBitmap(byte[] buffer);
        Task SendConfig();
        Task<BingoSessionInfo> GetConfig();
    }
}
