using Avalonia.Media;
using System.Threading.Tasks;

namespace BingoOnline.Interfaces
{
    public interface ISettings
    {
        Color ButtonFontColor { get; set; }
        Color P1_Clicked { get; set; }
        Color P1_NonClicked { get; set; }
        Color P2_Clicked { get; set; }

        void LoadSettings();
        void SaveSettings();
    }
}