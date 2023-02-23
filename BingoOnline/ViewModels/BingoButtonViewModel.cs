using Avalonia.Media;
using Avalonia.Themes.Fluent;
using Avalonia.Themes.Fluent.Controls;
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
        public readonly BingoButtonModel _button;
        private IBrush _buttonColor;

        public IBrush ButtonColor
        {
            get => _buttonColor;
            set => this.RaiseAndSetIfChanged(ref _buttonColor, value);
        }


        public string Text
        {
            get => _button.Text;
            set
            {
                _button.Text = value;
                this.RaisePropertyChanged("Text");
            }
        }

        public ICommand ButtonPress { get; set; }

        public BingoButtonViewModel(BingoButtonModel button)
        {
            

            _button = button;
            //  Text = $"Button - {_button.Number}";
            //ButtonPress = ReactiveCommand.Create(() =>
            //{
            //    _button?.ClickButton();
            //    Text = _button!.IsPressed ? $"Pressed" : "Not Pressed";
            //    ButtonColor = _button!.IsPressed ? Brushes.DarkGreen: Brushes.Black;
            //});
        }
    }
}
