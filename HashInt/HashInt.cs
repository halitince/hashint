using System.Text.Json.Serialization;
using HashidsNet;
using Microsoft.AspNetCore.Mvc;

namespace Hi.Types;

[JsonConverter(typeof(HashIntJsonConverter))]
[ModelBinder(BinderType = typeof(HashIntBinder))]
public class HashInt
{
    public static Hashids Hasher { get; set; }

    public string Value { get; }
    
    public override string ToString() => Value;
    public int ToInt() => GetId(Value);
    
    public HashInt() { }
    public HashInt(string value)
    {
        Value = value;
    }
    public HashInt(int? value)
    {
        Value = GetHash(value);
    }

    public static int GetId(string value) => string.IsNullOrWhiteSpace(value) ? 0 : Hasher.DecodeSingle(value);
    public static string GetHash(int? value) => value == null ? null : Hasher.Encode(value.Value);

    public static implicit operator int(HashInt hashInt) => GetId(hashInt?.Value);
    public static implicit operator HashInt(int value) => new(GetHash(value));

    public static implicit operator string(HashInt hashInt) => hashInt?.Value;
    public static implicit operator HashInt(string value) => new(value);
}

