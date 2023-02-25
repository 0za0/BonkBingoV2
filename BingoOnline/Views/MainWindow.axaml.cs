using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Mixins;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.ReactiveUI;
using BingoOnline.Interfaces;
using BingoOnline.ViewModels;
using ReactiveUI;
using ReactiveUI.Validation.Extensions;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reactive;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace BingoOnline.Views
{
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public MainWindow()
        {
            InitializeComponent();
            this.WhenActivated(d =>
            {
                d(ViewModel!.ShowAboutDialog.RegisterHandler(DoShowAboutDialogAsync));
                d(ViewModel!.ShowSettingsDialog.RegisterHandler(DoShowSettingsDialogAsync));
                d(this.BindValidation(ViewModel, x => x.UserNameText, x => x.UserNameValidation.Text));

            });
            //Kill the Logger
            Closing += (s, e) =>
            {

                logger.Info("Thank you, goodbye.");
                NLog.LogManager.Shutdown();
                
            };

        }

        private async Task DoShowSettingsDialogAsync(InteractionContext<SettingsViewModel, ISettings?> interaction)
        {
            var dialog = new SettingsWindow();
            dialog.DataContext = interaction.Input;

            var result = await dialog.ShowDialog<ISettings>(this);
            interaction.SetOutput(result);
        }

        private async Task DoShowAboutDialogAsync(InteractionContext<AboutViewModel, Unit?> interaction)
        {
            var dialog = new AboutWindow();
            dialog.DataContext = interaction.Input;

            var result = await dialog.ShowDialog<Unit>(this);
            interaction.SetOutput(result);
        }
    }
}
