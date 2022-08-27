using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PostCodeSearch.Serialization
{
    internal class NullableDoubleJsonConverter : JsonConverter<double?>
    {
        public override double? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.Number:
                    return reader.GetDouble();
                case JsonTokenType.String:
                    var stringVal = reader.GetString();
                    if (string.IsNullOrWhiteSpace(stringVal))
                    {
                        return null;
                    }
                    else
                    {
                        if (double.TryParse(stringVal, out var value))
                        {
                            return value;
                        }
                    }
                    break;
            }
            return null;
        }

        public override void Write(Utf8JsonWriter writer, double? value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNullValue();
            }
            else
            {
                writer.WriteNumberValue(value.Value);
            }
        }
    }
}
