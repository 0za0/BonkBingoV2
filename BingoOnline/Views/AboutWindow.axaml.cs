using Avalonia.Controls;
using Avalonia.ReactiveUI;
using BingoOnline.ViewModels;

namespace BingoOnline.Views
{
    public partial class AboutWindow : ReactiveWindow<AboutViewModel>
    {
        public AboutWindow()
        {
            InitializeComponent();
        }
    }
}
