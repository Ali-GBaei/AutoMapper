# 🗺️ AutoMapper Learning Project for ASP.NET Core MVC

A comprehensive, beginner to advanced level guide for learning AutoMapper in ASP.NET Core MVC. This project includes practical examples, detailed explanations, and best practices for using AutoMapper effectively.

<img width="1920" height="1080" alt="Screenshot 2025-11-01 010449" src="https://github.com/user-attachments/assets/b77539e1-4446-4746-9c85-a7e5d4dbc77c" />


## 📚 Table of Contents

- [What is AutoMapper?](#what-is-automapper)
- [Features](#features)
- [Installation](#installation)
- [Project Structure](#project-structure)
- [Learning Path](#learning-path)
- [Key Concepts](#key-concepts)
- [AutoMapper Methods & Attributes](#automapper-methods--attributes)
- [Advantages of AutoMapper](#advantages-of-automapper)
- [Best Practices](#best-practices)
- [Running the Project](#running-the-project)
- [Examples Included](#examples-included)

## 🎯 What is AutoMapper?

**AutoMapper** is a popular object-to-object mapper for .NET that eliminates the need for writing tedious manual mapping code between objects. It uses a convention-based approach to automatically map properties with the same names, while also supporting complex custom mappings.

### Why Use AutoMapper?

- ✅ **Reduces Boilerplate Code**: 90% less mapping code
- ✅ **Improves Maintainability**: Centralized mapping configuration
- ✅ **Separates Concerns**: Clean separation between domain models and DTOs
- ✅ **Performance Optimized**: Efficient mapping with minimal overhead
- ✅ **Supports Complex Scenarios**: Custom resolvers, type converters, conditional mapping

## 🌟 Features

This learning project covers:

### Beginner Level
1. **Basic Mapping** - Simple property-to-property mapping
2. **Reverse Mapping** - Bidirectional mapping between objects
3. **Collection Mapping** - Mapping lists, arrays, and collections

### Intermediate Level
4. **Custom Property Mapping** - Using ForMember for custom logic
5. **Nested Object Mapping** - Mapping complex object graphs
6. **Flattening** - Automatic flattening of nested properties

### Advanced Level
7. **Custom Value Resolvers** - Complex mapping logic with IValueResolver
8. **Custom Type Converters** - Type-to-type conversion with ITypeConverter
9. **Conditional Mapping** - Mapping based on conditions
10. **Inheritance Mapping** - Handling class hierarchies
11. **Projection (EF Core)** - Efficient database queries with ProjectTo
12. **Update Existing Objects** - Mapping to update existing entities

<img width="1920" height="1080" alt="Screenshot 2025-11-01 010539" src="https://github.com/user-attachments/assets/b8e96b66-9d87-492d-801f-15fd75c450b2" />


## 📦 Installation

### Prerequisites
- .NET 9.0 SDK or later
- Visual Studio 2022 / VS Code / JetBrains Rider

### Install AutoMapper Package

```bash
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
```

### Configure in Program.cs

```csharp
builder.Services.AddAutoMapper(typeof(Program).Assembly);
```

<img width="1920" height="1080" alt="Screenshot 2025-11-01 010554" src="https://github.com/user-attachments/assets/e41677d8-93d1-40bc-a9fd-2a5905278cdb" />


## 📁 Project Structure

```
AutoMapperLearning/
├── Controllers/
│   └── AutoMapperDemoController.cs    # All example actions
├── Models/
│   ├── Domain/
│   │   └── User.cs                    # Domain models (entities)
│   └── DTOs/
│       └── UserDTO.cs                 # Data Transfer Objects
├── Profiles/
│   └── MappingProfiles.cs             # AutoMapper configurations
├── CustomResolvers/
│   └── CustomResolvers.cs             # Custom value resolvers
├── CustomTypeConverters/
│   └── CustomTypeConverters.cs        # Custom type converters
└── Views/
    └── AutoMapperDemo/                # Example views
        ├── Index.cshtml               # Home page
        ├── Features.cshtml            # All features reference
        ├── Advantages.cshtml          # Benefits of AutoMapper
        └── BestPractices.cshtml       # Best practices guide
```

## 🎓 Learning Path

### 1. Start Here
Run the project and navigate to the home page to see all available examples organized by difficulty level.

### 2. Follow the Examples
Work through examples in order:
- Beginner (Green) → Intermediate (Yellow) → Advanced (Red)

### 3. Read the Documentation
- **Features Page**: Complete reference of all AutoMapper methods
- **Advantages Page**: Why and when to use AutoMapper
- **Best Practices Page**: How to use AutoMapper effectively

## 🔑 Key Concepts

### Domain Models
Represent your business entities and database tables. They contain all properties and business logic.

```csharp
public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}
```

### DTOs (Data Transfer Objects)
Lightweight objects designed to transfer data between layers. They expose only necessary data.

```csharp
public class UserDTO
{
    public int Id { get; set; }
    public string FullName { get; set; }  // Computed from FirstName + LastName
    public string Email { get; set; }
}
```

### Profiles
Configuration classes where you define your mappings.

```csharp
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDTO>()
            .ForMember(dest => dest.FullName, 
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
    }
}
```

### Using IMapper
Inject and use IMapper service in your controllers/services.

```csharp
public class MyController : Controller
{
    private readonly IMapper _mapper;
    
    public MyController(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    public IActionResult Index()
    {
        var user = GetUser();
        var userDTO = _mapper.Map<UserDTO>(user);
        return View(userDTO);
    }
}
```

## 🔧 AutoMapper Methods & Attributes

### Core Methods

| Method | Purpose | Example |
|--------|---------|---------|
| `CreateMap<TSource, TDestination>()` | Defines mapping | `CreateMap<User, UserDTO>();` |
| `Map<TDestination>(source)` | Maps to new object | `_mapper.Map<UserDTO>(user);` |
| `Map(source, destination)` | Updates existing object | `_mapper.Map(dto, user);` |
| `ReverseMap()` | Bidirectional mapping | `.ReverseMap();` |
| `ForMember()` | Custom property mapping | `.ForMember(dest => dest.FullName, ...)` |
| `ProjectTo<T>()` | EF Core projection | `query.ProjectTo<UserDTO>(...)` |

### ForMember Options

| Option | Purpose |
|--------|---------|
| `MapFrom()` | Custom source expression |
| `MapFrom<TResolver>()` | Use custom resolver |
| `Ignore()` | Ignore property |
| `Condition()` | Conditional mapping |
| `NullSubstitute()` | Default value for nulls |
| `ConvertUsing()` | Custom conversion logic |

### Profile Configuration

| Method | Purpose |
|--------|---------|
| `IncludeBase<TSource, TDest>()` | Include base class mappings |
| `BeforeMap()` | Execute before mapping |
| `AfterMap()` | Execute after mapping |
| `ForAllMembers()` | Apply to all members |

### Advanced Interfaces

- **IValueResolver**: Custom logic for property mapping
- **IMemberValueResolver**: More efficient resolver
- **ITypeConverter**: Global type-to-type conversion

## 🏆 Advantages of AutoMapper

### 1. Code Reduction
```csharp
// Without AutoMapper (20+ lines)
var dto = new UserDTO
{
    Id = user.Id,
    FirstName = user.FirstName,
    LastName = user.LastName,
    // ... many more properties
};

// With AutoMapper (1 line)
var dto = _mapper.Map<UserDTO>(user);
```

### 2. Maintainability
Adding new properties? AutoMapper handles it automatically if names match!

### 3. Performance
Nearly identical to manual mapping, but with ProjectTo() it can be even faster for database queries.

### 4. Security
Hide sensitive data by controlling what gets mapped to DTOs.

### 5. Testability
Mapping configurations can be validated with `AssertConfigurationIsValid()`.

## ✅ Best Practices

### Configuration
1. ✅ Use Profiles to organize mappings
2. ✅ Configure once at startup
3. ✅ Inject IMapper, not MapperConfiguration
4. ✅ Validate configuration in tests

### Mapping
5. ✅ Use convention-based mapping when possible
6. ✅ Use ForMember for custom logic
7. ✅ Use custom resolvers for complex logic
8. ✅ Map collections directly, not in loops
9. ✅ Use ProjectTo for database queries

### Architecture
10. ✅ Keep domain models pure
11. ✅ Use different DTOs for different purposes
12. ✅ Group profiles by domain/feature

## 🚀 Running the Project

### Using .NET CLI

```bash
cd AutoMapperLearning
dotnet restore
dotnet build
dotnet run
```

Then open your browser and navigate to: `https://localhost:5001` or `http://localhost:5000`

### Using Visual Studio
1. Open `AutoMapperLearning.csproj`
2. Press F5 to run
3. Browser will open automatically

## 📖 Examples Included

### Beginner Examples
- ✅ Basic Mapping (same property names)
- ✅ Reverse Mapping (bidirectional)
- ✅ Collection Mapping (lists, arrays)

### Intermediate Examples
- ✅ Custom Property Mapping (computed properties)
- ✅ Nested Object Mapping (complex objects)
- ✅ Flattening (nested to flat)

### Advanced Examples
- ✅ Custom Value Resolvers (complex logic)
- ✅ Custom Type Converters (type conversions)
- ✅ Conditional Mapping (based on conditions)
- ✅ Inheritance Mapping (base classes)
- ✅ Projection with EF Core (efficient queries)
- ✅ Update Existing Objects (partial updates)

## 📚 Additional Resources

- [Official AutoMapper Documentation](https://docs.automapper.org/)
- [AutoMapper GitHub Repository](https://github.com/AutoMapper/AutoMapper)
- Navigate through the web application for interactive examples

## 🤝 Contributing

This is a learning project. Feel free to:
- Add more examples
- Improve documentation
- Fix issues
- Share your experience

## 📄 License

This project is for educational purposes. Feel free to use and modify for learning.

## 🎓 What You'll Learn

After completing this tutorial, you will:
- ✅ Understand AutoMapper fundamentals
- ✅ Know when and how to use AutoMapper
- ✅ Master basic to advanced mapping scenarios
- ✅ Implement best practices in real projects
- ✅ Optimize performance with ProjectTo
- ✅ Handle complex mapping requirements
- ✅ Write maintainable and testable code

---

**Happy Mapping! 🗺️**

Start by running the application and exploring the examples in order from beginner to advanced!
