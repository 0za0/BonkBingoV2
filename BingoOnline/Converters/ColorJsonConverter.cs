using System;

using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using Avalonia.Media;

namespace BingoOnline.Converters
{
    //Why on Gods Green Earth is this not just default
    public class ColorJsonConverter : JsonConverter<Color>
    {
        public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => Color.Parse(reader.GetString());

        public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToString());
    }
}
