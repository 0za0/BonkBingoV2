using Avalonia.Media;
using BingoOnline.Interfaces;
using NLog;
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
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public readonly string SettingsPath = Path.Combine(Directory.GetCurrentDirectory(), "s.config");
        public Color P1_Clicked { get; set; }
        public Color P1_NonClicked { get; set; }
        public Color ButtonFontColor { get; set; }

        public Settings()
        {
        }
        public Settings(Color p1, Color p1n, Color font)
        {
            P1_Clicked = p1;
            P1_NonClicked = p1n;
            ButtonFontColor = font;
        }
        public void LoadSettings()
        {
            if (File.Exists(SettingsPath))
            {

                logger.Info("Loading Settings from {0}", SettingsPath);
                var allLines = File.ReadAllLines(SettingsPath);
                var settings = JsonSerializer.Deserialize<Settings>(string.Join("", allLines));
                if (settings != null)
                {
                    this.P1_Clicked = settings.P1_Clicked;
                    this.P1_NonClicked = settings.P1_NonClicked;
                    this.ButtonFontColor = settings.ButtonFontColor;
                }
            }
        }


        public void SaveSettings()
        {
            if (!File.Exists(SettingsPath))
            {
                using FileStream createStream = File.Create(SettingsPath);
                JsonSerializer.Serialize(createStream, this);
                createStream.DisposeAsync();
            }
            else
            {
                using FileStream createStream = File.Open(SettingsPath, FileMode.Open);
                JsonSerializer.Serialize(createStream, this);
                createStream.DisposeAsync();
            }
        }
    }
}
