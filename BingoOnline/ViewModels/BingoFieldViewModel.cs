using BingoOnline.Interfaces;
using BingoOnline.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingoOnline.ViewModels
{
    public class BingoFieldViewModel : ViewModelBase
    {
        private void GenerateButtons()
        {
            for (int i = 0; i < 25; i++)
            {
                var b = new BingoButtonViewModel(new BingoButtonModel(i));
                Buttons.Add(b);
            }
        }
        public ObservableCollection<BingoButtonViewModel> Buttons { get; set; }
        public BingoFieldViewModel(ISettings settings, INetworkService networkService)
        {
            Buttons = new();
            GenerateButtons();
        }


    }
}
