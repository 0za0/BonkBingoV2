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
    [Serializable]
    public class Settings : ISettings
    {
        public readonly string SettingsPath = Path.Combine(Directory.GetCurrentDirectory(), "s.config");
        public Color P1_Clicked { get; set; }
        public Color P1_NonClicked { get; set; }
        public Color ButtonFontColor { get; set; }

        public Settings()
        {
            ButtonFontColor = Color.FromArgb(255,0,255,0);
        }

        public void LoadSettings()
        {
            Debug.WriteLine("Settings Loaded!");
            File.ReadAllLinesAsync(SettingsPath);
        }

        public void SaveSettings()
        {
            using FileStream createStream = File.Create(SettingsPath);
            JsonSerializer.Serialize(createStream, this);
            createStream.DisposeAsync();
        }
    }
}
