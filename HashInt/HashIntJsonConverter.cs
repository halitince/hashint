using System.Text.Json;
using System.Text.Json.Serialization;

namespace Hi.Types;

public class HashIntJsonConverter : JsonConverter<HashInt>
{
    public override HashInt Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return new HashInt(reader.GetString()!);
    }
    
    public override void Write(Utf8JsonWriter writer, HashInt hashInt, JsonSerializerOptions options)
    {
        writer.WriteStringValue(hashInt.ToString());
    }
}