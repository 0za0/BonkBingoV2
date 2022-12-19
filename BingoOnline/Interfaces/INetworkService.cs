using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingoOnline.Interfaces
{
    public interface INetworkService
    {
        void RegisterPlayer(string key, string username);
        void SendKey(int index);
        void SendBitmap(byte[] buffer);
        void SendConfig();
    }
}
