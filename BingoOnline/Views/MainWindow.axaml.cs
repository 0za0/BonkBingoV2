using Avalonia.Controls;
using Avalonia.Controls.Mixins;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.ReactiveUI;
using BingoOnline.ViewModels;
using ReactiveUI;
using ReactiveUI.Validation.Extensions;
using System;
using System.Diagnostics;
using System.Reactive;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace BingoOnline.Views
{
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
            this.WhenActivated(d =>
            {
                d(ViewModel!.ShowAboutDialog.RegisterHandler(DoShowAboutDialogAsync));
                d(this.BindValidation(ViewModel, x => x.UserNameText, x => x.UserNameValidation.Text));
            });


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
