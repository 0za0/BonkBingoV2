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
using BingoOnline.Models;
using Splat;
using Microsoft.Extensions.DependencyInjection;
using BingoOnline.Interfaces;
using BingoOnline.Services;
using Avalonia.Media;

namespace BingoOnline.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IValidatableViewModel
    {

        #region Properties / Members and such
        private string _userNameText = "";
        public string UserNameText
        {
            get => _userNameText;
            set => this.RaiseAndSetIfChanged(ref _userNameText, value);
        }

        private INetworkService _networkService;

        //Additional ViewModels
        public BingoFieldViewModel BingoField { get; set; }

        //Commands
        public ICommand AboutCommand { get; }
        public ICommand SettingsCommand { get; }
        public ICommand ConnectCommand { get; }
        public ICommand PopoutBoardCommand { get; }


        //ReactiveUI Stuff
        public Interaction<AboutViewModel, Unit?> ShowAboutDialog { get; }
        public Interaction<SettingsViewModel, ISettings?> ShowSettingsDialog { get; }
        public ValidationContext ValidationContext { get; } = new ValidationContext();
        #endregion

        #region Constructors
        public MainWindowViewModel()
        {
            #region Shady DI Container
            var sc = new ServiceCollection();
            sc.AddSingleton<ISettings, Settings>()
                .AddSingleton<INetworkService, NetworkService>()
                .AddSingleton<BingoFieldViewModel>()
                .AddScoped<SettingsViewModel>()
                .AddSingleton<PopoutBoardWindow>();

            ServiceProvider sp = sc.BuildServiceProvider(new ServiceProviderOptions
            {
                ValidateOnBuild = true
            });
            _networkService = sp.GetRequiredService<INetworkService>();
            #endregion
            
            #region Validation Rules
            //Validation Rules for Inputs
            this.ValidationRule(viewModel => viewModel.UserNameText, name => !string.IsNullOrWhiteSpace(name), "Name shouldn't be null or white space.");
            this.ValidationRule(viewModel => viewModel.UserNameText, name => name!.All(char.IsLetterOrDigit), "Don't use special characters.");

            this.ValidationRule(viewModel => viewModel.UserNameText, name =>
            !(name!.Equals("null", StringComparison.InvariantCultureIgnoreCase) || name.Equals("undefined", StringComparison.InvariantCultureIgnoreCase)),
             "Very funny, but no.");
#if !DEBUG
            this.ValidationRule(viewModel => viewModel.UserNameText, name => !name.Equals("Nullpo", StringComparison.InvariantCultureIgnoreCase), "Reserved.");
#endif
            #endregion

            #region Dialogs
            //Dialogs
            ShowAboutDialog = new Interaction<AboutViewModel, Unit?>();
            ShowSettingsDialog = new Interaction<SettingsViewModel, ISettings?>();
            #endregion

            #region Additional ViewModels
            //Additional ViewModels
            BingoField = sp.GetRequiredService<BingoFieldViewModel>();
            #endregion

            #region Commands
            //Commands
            AboutCommand = ReactiveCommand.Create(async () =>
            {
                Debug.WriteLine("About Menu Pressed :)");
                var about = new AboutViewModel();
                await ShowAboutDialog.Handle(about);
            });
            SettingsCommand = ReactiveCommand.Create(async () =>
            {
                Debug.WriteLine("Settings Menu Opened");
                var settings = sp.GetRequiredService<SettingsViewModel>();
                await ShowSettingsDialog.Handle(settings);
            });
            PopoutBoardCommand = ReactiveCommand.Create(() =>
            {
                PopoutBoardWindow popoutBoardWindow = sp.GetRequiredService<PopoutBoardWindow>();
                popoutBoardWindow.Show();
            });
            ConnectCommand = ReactiveCommand.Create(() => { Debug.WriteLine("A"); }, this.IsValid());
            #endregion

        }
        #endregion

    }
}
