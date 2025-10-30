using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LISCareDTO.Helper
{
    public class CustomDateTimeConverter : JsonConverter<DateTime>
    {
        private readonly string _format = "dd-MM-yyyy";

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            if (DateTime.TryParseExact(value, _format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            {
                return date;
            }
            return DateTime.MinValue;
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(_format));
        }
    }
}
