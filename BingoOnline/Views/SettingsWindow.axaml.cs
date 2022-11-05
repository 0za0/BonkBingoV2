using Avalonia.Controls;
using Avalonia.ReactiveUI;
using BingoOnline.ViewModels;

namespace BingoOnline.Views
{
    public partial class SettingsWindow : ReactiveWindow<SettingsViewModel> 
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }
    }
}
