using Avalonia.Controls;
using Avalonia.Media;
using BingoOnline.Interfaces;
using BingoOnline.Models;
using BingoOnline.Services;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace BingoOnline.ViewModels
{
    public class BingoFieldViewModel : ViewModelBase
    {
        private void GenerateButtons(INetworkService ns, ISettings settings)
        {
            for (int i = 0; i < 25; i++)
            {
                var b = new BingoButtonViewModel(new BingoButtonModel(i));
                b.ButtonPress = ReactiveCommand.Create(async () =>
                {
                    b._button?.ClickButton();
                    b.Text = b._button!.IsPressed ? $"Pressed" : "Not Pressed";
                    b.ButtonColor = b._button!.IsPressed ? new SolidColorBrush(settings.P1_Clicked) : new SolidColorBrush(settings.P1_NonClicked);
                    if (ns.IsRegistered)
                        await ns.SendKey(b._button.Number, b._button.IsPressed);
                });
                Buttons.Add(b);

            }
        }

        internal void UpdateColors()
        {
            var fontBrush = new SolidColorBrush(_settings.ButtonFontColor);
            var pressedBrush = new SolidColorBrush(_settings.P1_Clicked);
            var bgBrush = new SolidColorBrush(_settings.P1_NonClicked);
            foreach (var item in Buttons)
            {
                item.ButtonFontColor = fontBrush;

                if (item._button.IsPressed)
                    item.ButtonColor = pressedBrush;
                else
                    item.ButtonColor = bgBrush;
            }
        }
        private readonly ISettings _settings;
        public ObservableCollection<BingoButtonViewModel> Buttons { get; set; }
        public BingoFieldViewModel(ISettings settings, INetworkService networkService)
        {
            Buttons = new();
            _settings = settings;
            GenerateButtons(networkService, settings);
        }


    }
}
