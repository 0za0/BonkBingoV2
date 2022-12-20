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
