using Avalonia.Controls;
using BingoOnline.ViewModels;

namespace BingoOnline.Views
{
    public partial class PopoutBoardWindow : Window
    {
        public PopoutBoardWindow(BingoFieldViewModel bfvm)
        {
            InitializeComponent();
            this.DataContext = bfvm;
        }
        public PopoutBoardWindow()
        {

        }
    }
}
