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
        private void GenerateButtons(INetworkService ns)
        {
            for (int i = 0; i < 25; i++)
            {
                var b = new BingoButtonViewModel(new BingoButtonModel(i));
                b.ButtonPress = ReactiveCommand.Create(async () =>
                {
                    b._button?.ClickButton();
                    b.Text = b._button!.IsPressed ? $"Pressed" : "Not Pressed";
                    b.ButtonColor = b._button!.IsPressed ? Brushes.DarkGreen : Brushes.Black;
                    if (ns.IsRegistered)
                        await ns.SendKey(b._button.Number, b._button.IsPressed);
                });
                Buttons.Add(b);

            }
        }


        public ObservableCollection<BingoButtonViewModel> Buttons { get; set; }
        public BingoFieldViewModel(ISettings settings, INetworkService networkService)
        {
            Buttons = new();

            GenerateButtons(networkService);
        }


    }
}
