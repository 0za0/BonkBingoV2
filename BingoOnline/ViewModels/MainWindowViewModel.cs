using BingoOnline.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Linq;
using ReactiveUI.Validation.Extensions;
using System.Windows.Input;
using ReactiveUI.Validation.Abstractions;
using ReactiveUI.Validation.Contexts;

namespace BingoOnline.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IValidatableViewModel
    {

        #region Properties and Fields
        //
        private string _userNameText = "";
        public string UserNameText
        {
            get => _userNameText;
            set => this.RaiseAndSetIfChanged(ref _userNameText, value);
        }

        //ViewModel Things
        public BingoFieldViewModel BingoField { get; set; }
        public ICommand AboutCommand { get; }
        public ICommand ConnectCommand { get; }
        public ICommand PopoutBoardCommand { get; }

        public Interaction<AboutViewModel, Unit?> ShowAboutDialog { get; }
        public Interaction<SettingsViewModel, Unit?> ShowSettingsDialog { get; }
        public ValidationContext ValidationContext { get; } = new ValidationContext();
        #endregion

        #region Constructors
        public MainWindowViewModel()
        {
            //Validation Rules for Inputs
            this.ValidationRule(viewModel => viewModel.UserNameText, name => !string.IsNullOrWhiteSpace(name), "Name shouldn't be null or white space.");
            this.ValidationRule(viewModel => viewModel.UserNameText, name => name.All(char.IsLetterOrDigit), "Don't use special characters, Fuckhead.");

            //Dialogs
            ShowAboutDialog = new Interaction<AboutViewModel, Unit?>();
            ShowSettingsDialog = new Interaction<SettingsViewModel, Unit?>();

            //Additional ViewModels
            BingoField = new BingoFieldViewModel();

            //Commands
            AboutCommand = ReactiveCommand.Create(async () =>
            {
                Debug.WriteLine("About Menu Pressed :)");
                var about = new AboutViewModel();
                await ShowAboutDialog.Handle(about);
            });
            PopoutBoardCommand = ReactiveCommand.Create(() =>
            {
                PopoutBoardWindow popoutBoardWindow = new PopoutBoardWindow();
                popoutBoardWindow.DataContext = BingoField;
                popoutBoardWindow.Show();
            });
            ConnectCommand = ReactiveCommand.Create(() => { Debug.WriteLine("A"); }, this.IsValid());
        }
        #endregion

    }
}
