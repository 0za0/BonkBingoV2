using BingoOnline.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingoOnline.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        public SettingsViewModel(ISettings settings)
        {
            Debug.WriteLine("SettingsViewModel Initialized");
        }
    }
}
