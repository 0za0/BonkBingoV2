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
        public SettingsWindow()
        {
            InitializeComponent();
        }

       
    }
}
