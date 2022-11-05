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
        private string _userNameText = "";
        public string UserNameText
        {
            get => _userNameText;
            set => this.RaiseAndSetIfChanged(ref _userNameText,value);
        }


        public BingoFieldViewModel BingoField { get; set; }
        public ICommand AboutCommand { get; }
        public ICommand PopoutBoardCommand { get; }
        public MainWindowViewModel()
        {
            this.ValidationRule(
            viewModel => viewModel.UserNameText,
            name => !string.IsNullOrWhiteSpace(name),
            "Name shouldn't be null or white space.");

            this.ValidationRule(
            viewModel => viewModel.UserNameText,
            name => name.All(char.IsLetterOrDigit),
            "Don't use special characters, Fuckhead.");

            ShowAboutDialog = new Interaction<AboutViewModel, Unit?>();
            BingoField = new BingoFieldViewModel();

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
        }
        public Interaction<AboutViewModel, Unit?> ShowAboutDialog { get; }

        public ValidationContext ValidationContext { get; } = new ValidationContext();
    }
}
