using Avalonia;
using Avalonia.Media;
using Avalonia.ReactiveUI;
using BingoOnline.Interfaces;
using BingoOnline.ViewModels;
using Egorozh.ColorPicker.Dialog;
using ReactiveUI;
using ReactiveUI.Validation.Extensions;
using System;
using System.Threading.Tasks;

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

            this.WhenActivated(d =>
            {
                d(ViewModel!.ShowColorPickerDialog.RegisterHandler(DoShowColorPickerDialog));

            });

        }
        private async Task DoShowColorPickerDialog(InteractionContext<ColorPickerDialog, Color> interaction)
        {
            var dialog = interaction.Input;

            var result = await dialog.ShowDialog<bool>(this);
            interaction.SetOutput(dialog.Color);
        }


    }
}
