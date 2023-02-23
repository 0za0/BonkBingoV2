using Avalonia;
using Avalonia.Media;
using BingoOnline.Interfaces;
using BingoOnline.Views;
using ReactiveUI;
using System.Diagnostics;

namespace BingoOnline.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {

        public static readonly StyledProperty<Color?> ColorProperty =
       AvaloniaProperty.Register<SettingsWindow, Color?>(nameof(ButtonFontColor));

        readonly ISettings _settings;
        public SettingsViewModel(ISettings settings)
        {
            _settings = settings;
            Debug.WriteLine("SettingsViewModel Initialized");
        }
        public void Save()
        {
            _settings.SaveSettings();
        }

        private Color _buttonFontColor;
        public Color ButtonFontColor
        {
            get => _buttonFontColor;
            set
            {
                this.RaiseAndSetIfChanged(ref _buttonFontColor, value);
                //_settings.ButtonFontColor = value;
                //this.RaisePropertyChanged("ButtonFontColor");
                //_settings.SaveSettings();

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
