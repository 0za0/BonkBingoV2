using Avalonia.Media;
using BingoOnline.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BingoOnline.Models
{
    //User DI for this
    public class Settings : ISettings
    {
        public readonly string SettingsPath = Path.Combine(Directory.GetCurrentDirectory(), ".config");
        public Color P1_Clicked { get; private set; }
        public Color P1_NonClicked { get; private set; }
        public Color ButtonFontColor { get; private set; }
        public Color P2_Clicked { get; private set; }

        public async Task LoadSettings()
        {
            Debug.WriteLine("Settings Loaded!");
           await File.ReadAllLinesAsync(SettingsPath);
        }

        public async Task SaveSettings()
        {
            using FileStream createStream = File.Create(SettingsPath);
            await JsonSerializer.SerializeAsync(createStream, this);
            await createStream.DisposeAsync();
        }
    }
}
