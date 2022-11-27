using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BingoOnline.Models
{
    //User DI for this
    public class Settings
    {
        public readonly string SettingsPath = Path.Combine(Directory.GetCurrentDirectory(),".config");
        public Color P1_Clicked { get; private set; }
        public Color P1_NonClicked { get; private set; }
        public Color ButtonFontColor { get; private set; }
        public Color P2_Clicked { get; private set; }

        public async Task LoadSettings()
        {
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
