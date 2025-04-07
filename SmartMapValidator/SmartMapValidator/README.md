# SmartMapValidator

🔧 **SmartMapValidator** is a lightweight, attribute-based object mapping and validation library for C#. It helps you map DTOs to entities and validate properties using simple custom attributes.

---

## 🚀 Features

- ✅ Custom mapping with `[MapTo]` attribute  
- 🚫 Skip mapping with `[MapIgnore]` attribute  
- 📋 Built-in validation support with `[Required]` and `[Range]` attributes  
- 🧩 Modular and extensible architecture  

---

## 📦 Installation

You can use it locally as a class library or package it as a NuGet package.

### 🔹 Add project reference manually:

```bash
dotnet add reference ../SmartMapValidator/SmartMapValidator.csproj
```

---

## 🧪 Usage Example

### Define a DTO and Entity:

```csharp
public class UserDto
{
    [Required]
    public string Name { get; set; }

    [MapTo("UserAge")]
    [Range(18, 99)]
    public int Age { get; set; }

    [MapIgnore]
    public string Secret { get; set; }
}

public class UserEntity
{
    public string Name { get; set; }
    public int UserAge { get; set; }
}
```

### Mapping:

```csharp
var dto = new UserDto
{
    Name = "Aysel",
    Age = 25,
    Secret = "Hidden"
};

var entity = SmartMap.Map<UserDto, UserEntity>(dto);

// entity.Name -> "Aysel"
// entity.UserAge -> 25
```

### Validation:

```csharp
var invalidDto = new UserDto
{
    Name = "",
    Age = 10
};

var result = SmartMap.Validate(invalidDto);

if (!result.IsValid)
{
    Console.WriteLine(result.ToString());
    // Output:
    // Name field cannot be empty.
    // Age must be between 18 and 99.
}
```

---

## 🧱 Project Structure

```
SmartMapValidator/
├── Attributes/
│   ├── MapToAttribute.cs
│   ├── MapIgnoreAttribute.cs
├── Core/
│   ├── Mapper.cs
│   ├── Validator.cs
│   ├── ValidationResult.cs
├── Interfaces/
│   ├── IMapDto.cs
├── SmartMap.cs
├── SmartMapValidator.csproj
```

---

## 🛠️ Planned Features

- [ ] `[Regex]` and `[StringLength]` validation attributes  
- [ ] Nested object mapping support  
- [ ] Optional AutoMapper integration  

---

## 📄 License

This project is open-source and licensed under the MIT License.

---

## ✨ Author

Created by **Rashad Aghayev**  
Contributions, pull requests, and suggestions are welcome! 😊

## 📫 Contact

If you have any questions or suggestions, feel free to contact me at:  
📧 rashadaghayev85@gmail.com  
📱 +994 70 818 17 00
