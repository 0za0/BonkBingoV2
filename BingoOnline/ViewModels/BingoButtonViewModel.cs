using BingoOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingoOnline.ViewModels
{
    public class BingoButtonViewModel : ViewModelBase
    {
        private readonly BingoButtonModel _button;
        public string Text => _button.Text;


        public BingoButtonViewModel(BingoButtonModel button)
        {
            _button = button;
        }
    }
}
