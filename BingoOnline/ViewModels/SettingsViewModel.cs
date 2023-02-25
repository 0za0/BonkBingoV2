using Avalonia;
using Avalonia.Media;
using BingoOnline.Interfaces;
using BingoOnline.Views;
using Egorozh.ColorPicker.Dialog;
using ReactiveUI;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Input;

namespace BingoOnline.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {


        readonly ISettings _settings;
        
        #region Properties
        public ICommand FontColorButtonClick { get; }
        public ICommand BackgroundButtonClick { get; }
        public ICommand BackgroundUnclickedButtonClick { get; }
        public Interaction<ColorPickerDialog, Color> ShowColorPickerDialog { get; }

        private IBrush _buttonFontColorBrush;
        public IBrush ButtonFontColorBrush
        {
            get { return _buttonFontColorBrush; }
            set => this.RaiseAndSetIfChanged(ref _buttonFontColorBrush, value);
        }

        private IBrush _buttonBackgroundColorBrush;
        public IBrush ButtonBackGroundColorBrush
        {
            get { return _buttonBackgroundColorBrush; }
            set => this.RaiseAndSetIfChanged(ref _buttonBackgroundColorBrush, value);
        }

        private IBrush _buttonBackgroundNonClickedColorBrush;
        public IBrush ButtonBackGroundNonClickedColorBrush
        {
            get { return _buttonBackgroundNonClickedColorBrush; }
            set => this.RaiseAndSetIfChanged(ref _buttonBackgroundNonClickedColorBrush, value);
        }

        public Color ButtonFontColor
        {
            get => _settings.ButtonFontColor;
            set
            {

                _settings.ButtonFontColor = value;
                this.RaisePropertyChanged("ButtonFontColor");
                if (ButtonFontColorBrush != null)
                    ButtonFontColorBrush = new SolidColorBrush(ButtonFontColor);
            }
        }

        public Color P1_Clicked
        {
            get => _settings.P1_Clicked;
            set
            {
                _settings.P1_Clicked = value;
                this.RaisePropertyChanged("P1_Clicked");
                if (ButtonBackGroundColorBrush != null)
                    ButtonBackGroundColorBrush = new SolidColorBrush(P1_Clicked);
            }
        }

        public Color P1_NonClicked
        {
            get => _settings.P1_NonClicked;
            set
            {
                _settings.P1_NonClicked = value;
                this.RaisePropertyChanged("P1_NonClicked");
                if (ButtonBackGroundNonClickedColorBrush != null)
                    ButtonBackGroundNonClickedColorBrush = new SolidColorBrush(P1_NonClicked);
            }
        }
        #endregion


        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public SettingsViewModel(ISettings settings)
        {
            _settings = settings;
            Debug.WriteLine("SettingsViewModel Initialized");
            #region ColorPickerBullshittery
            ShowColorPickerDialog = new Interaction<ColorPickerDialog, Color>();

            ButtonFontColorBrush = new SolidColorBrush(ButtonFontColor, 1.0);
            ButtonBackGroundColorBrush = new SolidColorBrush(P1_Clicked, 1.0);
            ButtonBackGroundNonClickedColorBrush = new SolidColorBrush(P1_NonClicked, 1.0);


            FontColorButtonClick = ReactiveCommand.Create(async () =>
            {
                ColorPickerDialog dialog = new()
                {
                    Color = ButtonFontColor,
                };
                ButtonFontColor = await ShowColorPickerDialog.Handle(dialog);
                logger.Info("ButtonFontColor has changed, it is now {0}", ButtonFontColor.ToString());
            });


            BackgroundButtonClick = ReactiveCommand.Create(async () =>
            {
                ColorPickerDialog dialog = new()
                {
                    Color = P1_Clicked,
                };
                P1_Clicked = await ShowColorPickerDialog.Handle(dialog);
                logger.Info("P1_Clicked has changed, it is now {0}", P1_Clicked.ToString());
            });


            BackgroundUnclickedButtonClick = ReactiveCommand.Create(async () =>
            {
                ColorPickerDialog dialog = new()
                {
                    Color = P1_NonClicked,
                };
                P1_NonClicked = await ShowColorPickerDialog.Handle(dialog);
                logger.Info("P1_NonClicked has changed, it is now {0}", P1_NonClicked.ToString());
            });
            #endregion
        }
        public void Save()
        {
            _settings.SaveSettings();
        }
    }
}
