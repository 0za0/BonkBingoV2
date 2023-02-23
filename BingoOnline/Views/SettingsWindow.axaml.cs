using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using BingoOnline.ViewModels;
using ReactiveUI;
using System;

namespace BingoOnline.Views
{
    public partial class SettingsWindow : ReactiveWindow<SettingsViewModel>
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public SettingsWindow()
        {
            InitializeComponent();
            Closing += (s, e) =>
            {
                logger.Info("Settings Window Closing. Saving Settings...");
                ViewModel!.Save();
            };

        }


    }
}
