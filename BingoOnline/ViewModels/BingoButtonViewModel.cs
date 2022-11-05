using BingoOnline.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace BingoOnline.ViewModels
{
    public class BingoButtonViewModel : ViewModelBase
    {
        readonly BingoButtonModel _button;
        public string Text {
            get => _button.Text;
            set {
                _button.Text = value;
                this.RaisePropertyChanged("Text");
            }
        }
      
        public ICommand ButtonPress { get; }

        public BingoButtonViewModel(BingoButtonModel button)
        {
            Debug.WriteLine("Hello?");

            _button = button;
          //  Text = $"Button - {_button.Number}";
            ButtonPress = ReactiveCommand.Create(() =>
            {
                _button?.ClickButton();
                Text = _button!.IsPressed? $"Pressed":"Not Pressed";
            });
        }
    }
}
