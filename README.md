# HashInt
Welcome to HashInt Type



```csharp
using HashidsNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MyNamespace;

[ModelBinder(BinderType = typeof(HashIntBinder))]
public class HashInt
{
    private static readonly Hashids Hasher = new("your.salt:)", 8);

    public string Value { get; }
    public int Int => GetId(Value);

    public override string ToString() => Value;

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
```
