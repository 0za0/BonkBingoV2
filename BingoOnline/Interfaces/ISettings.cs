using Avalonia.Media;
using System.Threading.Tasks;

namespace BingoOnline.Interfaces
{
    public interface ISettings
    {
        Color ButtonFontColor { get; }
        Color P1_Clicked { get; }
        Color P1_NonClicked { get; }
        Color P2_Clicked { get; }

        Task LoadSettings();
        Task SaveSettings();
    }
}