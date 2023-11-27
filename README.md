# HashInt

`TR` "HashInt" adını verdiğimiz bu özel veri tipi, uygulamanızda kullanılan ID değerlerini daha güvenli hale getirmeyi amaçlar. Bu tip, ID değerlerini arayüzde görünmez hale getirerek verilerin gizliliğini ve güvenliğini artırır. Ayrıca, bu yaklaşımı kullanarak tüm ID değerlerini otomatik olarak şifrelemiş olursunuz.
Özetle, "HashInt" veri tipi kullanımı, hassas ID verilerini koruma altına almak ve kullanıcıların veya kötü niyetli kişilerin bu verilere erişimini zorlaştırmak için kullanılır. Bu, veri gizliliğini artırmanıza ve uygulamanızın güvenliğini sağlamanıza yardımcı olabilir.

`EN` The custom data type named "HashInt" is designed to enhance the security of the ID values used within your application. This type conceals ID values from the user interface, thereby bolstering data privacy and security. Additionally, by adopting this approach, all ID values are automatically encrypted.
In summary, the use of the "HashInt" data type serves the purpose of safeguarding sensitive ID data and making it challenging for users or malicious actors to access these data. This can help you strengthen data privacy and ensure the security of your application.

## Installation

Install the package with [NuGet]

    Install-Package HashInt

## Usage

```C#
app.UseHashInt(new Hashids("your.Salt", 8));
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


## Sample

```csharp
private List<MyEntity> TestDataLikeDbTable => new()
{

    new MyEntity { Id = 1, Name = "Test 1" },
    new MyEntity { Id = 2, Name = "Test 2" },
    new MyEntity { Id = 3, Name = "Test 3" },
    new MyEntity { Id = 4, Name = "Test 4" },
    new MyEntity { Id = 5, Name = "Test 5" }
};

[HttpGet("TestList")]
public List<MyModel> TestList()
{
    var list = TestDataLikeDbTable.Select(x => new MyModel()
    {
        Id = x.Id,
        Name = x.Name,
    }).ToList();

    return list;
}

```

## Output

```url
http://domain.com/TestList
```

```json

[
  {
    "id": "Y8q3Ymvr",
    "name": "Test 1"
  },
  {
    "id": "nrz5Pm79",
    "name": "Test 2"
  },
  {
    "id": "r5zP4z9A",
    "name": "Test 3"
  },
  {
    "id": "MDmRlqPo",
    "name": "Test 4"
  },
  {
    "id": "xnmYlZPy",
    "name": "Test 5"
  }
]

```

