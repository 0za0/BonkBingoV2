using Avalonia.Media;
using BingoOnline.Converters;
using BingoOnline.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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
        [JsonConstructor]
        public Settings(Color P1_Clicked, Color P1_NonClicked, Color ButtonFontColor)
        {
            this.P1_Clicked = P1_Clicked;
            this.P1_NonClicked = P1_NonClicked;
            this.ButtonFontColor = ButtonFontColor;

        }
        public void LoadSettings()
        {
            if (File.Exists(SettingsPath))
            {

                var options = new JsonSerializerOptions()
                { Converters = { new ColorJsonConverter() } };

                logger.Info("Loading Settings from {0}", SettingsPath);
                var allLines = File.ReadAllLines(SettingsPath);

                var settings = JsonSerializer.Deserialize<Settings>(string.Join("", allLines), options);
                if (settings != null)
                {
                    logger.Debug("De-Serialized Settings, reading them now");
                    this.P1_Clicked = settings.P1_Clicked;
                    this.P1_NonClicked = settings.P1_NonClicked;
                    this.ButtonFontColor = settings.ButtonFontColor;

                    logger.Debug("P1_Clicked is {0}", P1_Clicked);
                    logger.Debug("P1_NonClicked is {0}", P1_NonClicked);
                    logger.Debug("ButtonFontColor is {0}", ButtonFontColor);

                }
            }
        }


        public void SaveSettings()
        {
            if (File.Exists(SettingsPath))
                File.Delete(SettingsPath);

            var options = new JsonSerializerOptions()
            { Converters = { new ColorJsonConverter() } };
            using FileStream createStream = File.Create(SettingsPath);
            JsonSerializer.Serialize(createStream, this, options);
            createStream.DisposeAsync();
        }
    }
}
