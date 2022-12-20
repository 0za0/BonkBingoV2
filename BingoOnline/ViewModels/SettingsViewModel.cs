using Avalonia.Controls;
using Avalonia.Media;
using BingoOnline.Interfaces;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace BingoOnline.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        readonly ISettings _settings;
        public SettingsViewModel(ISettings settings)
        {
            _settings = settings;
            Debug.WriteLine("SettingsViewModel Initialized");
        }

        public Color ButtonFontColor
        {
            get => _settings.ButtonFontColor;
            set
            {
                _settings.ButtonFontColor = value;
                this.RaisePropertyChanged("ButtonFontColor");
                _settings.SaveSettings();
            }
        }

        public Color P1_Clicked
        {
            get => _settings.P1_Clicked;
            set
            {
                _settings.P1_Clicked = value;
                this.RaisePropertyChanged("P1_Clicked");
                _settings.SaveSettings();
            }
        }

        public Color P1_NonClicked
        {
            get => _settings.P1_NonClicked;
            set
            {
                _settings.P1_NonClicked = value;
                this.RaisePropertyChanged("P1_NonClicked");
                _settings.SaveSettings();
            }
        }
    }
}
