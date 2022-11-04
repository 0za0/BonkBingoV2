using System;
using System.Collections.Generic;
using System.Text;

namespace BingoOnline.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public BingoFieldViewModel BingoField { get; set; }
        public MainWindowViewModel()
        {
            BingoField = new BingoFieldViewModel();
        }
    }
}
