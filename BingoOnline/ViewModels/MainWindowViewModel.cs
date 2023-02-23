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
using Avalonia.Controls;
using MessageBox.Avalonia.DTO;
using NLog;

namespace BingoOnline.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IValidatableViewModel
    {

        #region Properties / Members and such

        public bool IsRegistered
        {
            get => _networkService.IsRegistered;
            set
            {
                _networkService.IsRegistered = value;
                this.RaisePropertyChanged("IsRegistered");
            }
        }

        private string _userNameText = "";
        public string UserNameText
        {
            get => _userNameText;
            set => this.RaiseAndSetIfChanged(ref _userNameText, value);
        }

        private string _keyText = "";
        public string KeyText
        {
            get => _keyText;
            set => this.RaiseAndSetIfChanged(ref _keyText, value);
        }

        private readonly INetworkService _networkService;

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
            //Logger Init   

            //TODO: Setup XML based Config for archiving old files 
            NLog.LogManager.Setup().LoadConfiguration(builder => {
                builder.ForLogger().FilterMinLevel(NLog.LogLevel.Debug).WriteToConsole();
                builder.ForLogger().FilterMinLevel(NLog.LogLevel.Debug).WriteToFile(fileName: "Log.fuck");
            });

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
            
            //LOAD SETTINGS
            sp.GetRequiredService<ISettings>().LoadSettings();

            
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
                popoutBoardWindow.Closing += (s, e) =>
                {
                    if (s != null) //Only did this to get rid of the warning
                    {
                        ((Window)s).Hide();
                        e.Cancel = true;
                    }
                };
                popoutBoardWindow.Show();
            });
            //What The Fuck
            var canExecute = this.WhenAnyValue(x => x.IsRegistered, y => y == false);

            ConnectCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                StatusError success = await _networkService.RegisterPlayer(KeyText, UserNameText);
                if (success.Success)
                    IsRegistered = true; //Pretty sure there is a better way of doing this
                else
                {
                    var contentMessage = (success.StatusCode != 999) ? success.ErrorMessage : "Server cannot be reached ... It might've exploded or something ... dunno. Contact Nullpo";
                    var messageBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow(new MessageBoxStandardParams
                    {

                        ButtonDefinitions = MessageBox.Avalonia.Enums.ButtonEnum.Ok,
                        ContentTitle = $"What? Error: {success.StatusCode}",
                        ContentMessage = contentMessage,
                    });
                    await messageBoxStandardWindow.Show();
                }

            },
             Observable.CombineLatest(canExecute, this.IsValid(), (a, b) => a && b));
            #endregion

        }
        #endregion

    }
}
