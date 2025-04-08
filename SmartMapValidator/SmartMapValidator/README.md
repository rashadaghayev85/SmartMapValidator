# SmartMapValidator

🔧 **SmartMapValidator** is a lightweight, attribute-based object mapping and validation library for C#. It helps you map DTOs to entities and validate object properties using simple custom attributes.

---

## 🚀 Features

- ✅ **Attribute-based mapping** with `[MapTo]`
- 🚫 **Ignore properties** with `[MapIgnore]`
- 📋 **Built-in validation attributes**:
  - `[Required]` — ensures field is not null or empty
  - `[Range(min, max)]` — numeric range validation
  - `[Regex(pattern)]` — validates against a regular expression
  - `[MinLength(length)]` / `[MaxLength(length)]` — string length limits
- 🔄 Nullable type support (e.g., `int?`, `DateTime?`)
- 🧩 Modular, extensible, clean architecture

---

## 📦 Installation

You can use the library locally as a class library, or publish it to NuGet.

### 🔹 Add as a project reference

```bash
dotnet add reference ../SmartMapValidator/SmartMapValidator.csproj
```

---

## 🧪 Usage Example

### DTO and Entity Definitions

```csharp
public class UserDto
{
    [Required]
    public string Name { get; set; }

    [Range(18, 99)]
    [MapTo("UserAge")]
    public int? Age { get; set; }

    [Regex(@"^\S+@\S+\.\S+$")]
    public string Email { get; set; }

    [MinLength(6)]
    [MaxLength(20)]
    public string Username { get; set; }

    [MapIgnore]
    public string Secret { get; set; }
}

public class UserEntity
{
    public string Name { get; set; }
    public int UserAge { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
}
```

---

### Mapping

```csharp
var dto = new UserDto
{
    Name = "Rashad",
    Age = 28,
    Email = "rashad@example.com",
    Username = "rashad_28",
    Secret = "TopSecret"
};

var entity = SmartMap.Map<UserDto, UserEntity>(dto);

// entity.Name -> "Rashad"
// entity.UserAge -> 28
// entity.Email -> "rashad@example.com"
```

---

### Validation

```csharp
var invalidDto = new UserDto
{
    Name = "",
    Age = 15,
    Email = "invalid_email",
    Username = "abc"
};

var result = SmartMap.Validate(invalidDto);

if (!result.IsValid)
{
    Console.WriteLine(result.ToString());
    // Output:
    // Name is required.
    // Age must be between 18 and 99.
    // Email format is invalid.
    // Username must be at least 6 characters.
}
```

---

## 🛠️ Planned Features

- [ ] Nested object mapping
- [ ] Collection support (e.g., `List<T>`)
- [ ] AutoMapper integration

---

## 📄 License

Licensed under the MIT License.

---

## ✨ Author

Created with  by **Rashad Aghayev**  
Pull requests and contributions are always welcome!

📧 rashadaghayev85@gmail.com  
📱 +994 70 818 17 00