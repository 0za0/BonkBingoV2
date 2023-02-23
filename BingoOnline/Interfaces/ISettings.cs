using Avalonia.Media;
using System.Threading.Tasks;

namespace BingoOnline.Interfaces
{
    public interface ISettings
    {
        Color ButtonFontColor { get; set; }
        Color P1_Clicked { get; set; }
        Color P1_NonClicked { get; set; }

        void LoadSettings();
        void SaveSettings();
    }
}