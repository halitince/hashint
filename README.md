# HashInt

`TR` "HashInt" adını verdiğimiz bu özel veri tipi, uygulamanızda kullanılan ID değerlerini daha güvenli hale getirmeyi amaçlar. Bu tip, ID değerlerini arayüzde görünmez hale getirerek verilerin gizliliğini ve güvenliğini artırır. Ayrıca, bu yaklaşımı kullanarak tüm ID değerlerini otomatik olarak şifrelemiş olursunuz.
Özetle, "HashInt" veri tipi kullanımı, hassas ID verilerini koruma altına almak ve kullanıcıların veya kötü niyetli kişilerin bu verilere erişimini zorlaştırmak için kullanılır. Bu, veri gizliliğini artırmanıza ve uygulamanızın güvenliğini sağlamanıza yardımcı olabilir.

`EN` The custom data type named "HashInt" is designed to enhance the security of the ID values used within your application. This type conceals ID values from the user interface, thereby bolstering data privacy and security. Additionally, by adopting this approach, all ID values are automatically encrypted.
In summary, the use of the "HashInt" data type serves the purpose of safeguarding sensitive ID data and making it challenging for users or malicious actors to access these data. This can help you strengthen data privacy and ensure the security of your application.

## Installation

Install the package with [NuGet]

    Install-Package hashids.net

## Usage

```C#
using HashidsNet;
```



## Source

```csharp
[JsonConverter(typeof(HashIntJsonConverter))]
[ModelBinder(BinderType = typeof(HashIntBinder))]
public class HashInt
{
    private static readonly Hashids Hasher = new("your.salt:)", 8);

    public string Value { get; }
    
    public override string ToString() => Value;
    public int ToInt () => GetId(Value);

    public HashInt() { }
    public HashInt(string value)
    {
        Value = value;
    }
    public HashInt(int? value)
    {
        Value = GetHash(value);
    }

    private static int GetId(string value) => string.IsNullOrWhiteSpace(value) ? 0 : Hasher.DecodeSingle(value);
    private static string GetHash(int? value) => value == null ? null : Hasher.Encode(value.Value);

    public static implicit operator int(HashInt hashInt) => GetId(hashInt?.Value);
    public static implicit operator HashInt(int value) => new(GetHash(value));

    public static implicit operator string(HashInt hashInt) => hashInt.Value;
    public static implicit operator HashInt(string value) => new(value);
}

public class HashIntBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
            throw new ArgumentNullException(nameof(bindingContext));

        var modelName = bindingContext.ModelName;

        var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);
        if (valueProviderResult == ValueProviderResult.None)
            return Task.CompletedTask;

        bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

        var value = valueProviderResult.FirstValue;

        if (string.IsNullOrEmpty(value))
            return Task.CompletedTask;

        bindingContext.Result = ModelBindingResult.Success(new HashInt(value));
        return Task.CompletedTask;
    }
}

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
```



## Sample Model

```C#
public class MyEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class MyModel
{
    public HashInt Id { get; set; }
    public string Name { get; set; }
}
```
