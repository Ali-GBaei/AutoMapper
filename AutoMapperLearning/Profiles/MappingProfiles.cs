using AutoMapper;
using AutoMapperLearning.Models.Domain;
using AutoMapperLearning.Models.DTOs;
using AutoMapperLearning.CustomResolvers;
using AutoMapperLearning.CustomTypeConverters;

namespace AutoMapperLearning.Profiles
{
    /// <summary>
    /// AutoMapper Profile: UserProfile
    /// 
    /// A Profile is a configuration class where you define your mappings.
    /// Best Practice: Create separate profiles for different domains/contexts
    /// 
    /// Profile Benefits:
    /// - Organizes mapping configurations
    /// - Separates concerns
    /// - Makes mappings testable
    /// - Allows for profile-specific configurations
    /// </summary>
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // ========================================
            // BEGINNER LEVEL MAPPINGS
            // ========================================

            // 1. BASIC MAPPING - Same property names
            // AutoMapper automatically maps properties with the same name and compatible types
            CreateMap<Employee, EmployeeDTO>();

            // 2. REVERSE MAPPING
            // ReverseMap() creates a two-way mapping (Source <-> Destination)
            // Useful when you need to map in both directions
            CreateMap<CreateUserDTO, User>()
                .ReverseMap();

            // ========================================
            // INTERMEDIATE LEVEL MAPPINGS
            // ========================================

            // 3. CUSTOM PROPERTY MAPPING with ForMember
            // ForMember() allows you to customize how individual properties are mapped
            CreateMap<User, UserDTO>()
                // Map FullName from concatenating FirstName and LastName
                .ForMember(
                    dest => dest.FullName,  // Destination property
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}")) // Source mapping
                
                // Format DateOfBirth to string
                .ForMember(
                    dest => dest.DateOfBirthFormatted,
                    opt => opt.MapFrom(src => src.DateOfBirth.ToString("MMMM dd, yyyy")))
                
                // Calculate Age using custom resolver
                .ForMember(
                    dest => dest.Age,
                    opt => opt.MapFrom<AgeResolver>()) // Using custom value resolver
                
                // Map TotalOrders count
                .ForMember(
                    dest => dest.TotalOrders,
                    opt => opt.MapFrom(src => src.Orders.Count))
                
                // Use custom resolver for status
                .ForMember(
                    dest => dest.Status,
                    opt => opt.MapFrom<UserStatusResolver>());

            // 4. NESTED OBJECT MAPPING
            // AutoMapper automatically maps nested objects if mappings are defined
            CreateMap<Address, AddressDTO>()
                // Custom resolver for FullAddress
                .ForMember(
                    dest => dest.FullAddress,
                    opt => opt.MapFrom<FullAddressResolver>());

            // 5. COLLECTION MAPPING
            // AutoMapper automatically maps collections (List, Array, IEnumerable, etc.)
            CreateMap<Order, OrderDTO>()
                .ForMember(
                    dest => dest.OrderDateFormatted,
                    opt => opt.MapFrom(src => src.OrderDate.ToString("MM/dd/yyyy")))
                .ForMember(
                    dest => dest.ItemCount,
                    opt => opt.MapFrom(src => src.Items.Count));

            // ========================================
            // ADVANCED LEVEL MAPPINGS
            // ========================================

            // 6. CONDITIONAL MAPPING
            // MapFrom with condition - only map if condition is true
            CreateMap<User, UserBasicDTO>()
                .ForMember(
                    dest => dest.FullName,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(
                    dest => dest.Email,
                    opt => opt.Condition(src => src.IsActive)); // Only map email if user is active

            // 7. IGNORE PROPERTIES
            // Ignore() tells AutoMapper to skip mapping a specific property
            CreateMap<User, UserViewModel>()
                .ForMember(
                    dest => dest.FullName,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(
                    dest => dest.Age,
                    opt => opt.MapFrom(src => CalculateAge(src.DateOfBirth).ToString()))
                .ForMember(
                    dest => dest.Address,
                    opt => opt.MapFrom(src => 
                        src.Address != null 
                            ? $"{src.Address.City}, {src.Address.State}" 
                            : "No address"))
                .ForMember(
                    dest => dest.OrderSummary,
                    opt => opt.MapFrom((src, dest, destMember, context) => 
                    {
                        if (src.Orders == null || !src.Orders.Any())
                            return "No orders yet";
                        var totalOrders = src.Orders.Count;
                        var totalSpent = src.Orders.Sum(o => o.TotalAmount);
                        return $"{totalOrders} orders - Total: ${totalSpent:N2}";
                    }))
                .ForMember(
                    dest => dest.AccountStatus,
                    opt => opt.MapFrom(src => src.IsActive ? "Active" : "Inactive"));

            // 8. NULL SUBSTITUTION
            // NullSubstitute() provides a default value when source property is null
            CreateMap<Address, string>()
                .ConvertUsing(src => src != null 
                    ? $"{src.Street}, {src.City}, {src.State}" 
                    : "No Address Available");

            // 9. BEFORE/AFTER MAP Actions
            // BeforeMap() and AfterMap() allow you to execute custom logic
            CreateMap<User, UserDTO>()
                .BeforeMap((src, dest) =>
                {
                    // Logic to execute before mapping
                    // Example: Logging, validation, etc.
                    Console.WriteLine($"Mapping user: {src.FirstName} {src.LastName}");
                })
                .AfterMap((src, dest) =>
                {
                    // Logic to execute after mapping
                    // Example: Additional calculations, cleanup, etc.
                    Console.WriteLine($"Mapped to DTO with ID: {dest.Id}");
                });

            // 10. INHERITANCE MAPPING
            // IncludeBase() or IncludeMembers() for inheritance scenarios
            CreateMap<Manager, ManagerDTO>()
                .IncludeBase<Employee, EmployeeDTO>(); // Include base class mappings
        }

        // Helper method for age calculation
        private int CalculateAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > today.AddYears(-age)) age--;
            return age;
        }
    }

    /// <summary>
    /// AutoMapper Profile: ProductProfile
    /// Demonstrates flattening and projection
    /// </summary>
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            // ========================================
            // FLATTENING
            // ========================================
            
            // AUTOMATIC FLATTENING
            // AutoMapper automatically flattens nested properties
            // It looks for properties like:
            // - CategoryName -> maps from Category.Name
            // - CategoryDescription -> maps from Category.Description
            CreateMap<Product, ProductDTO>()
                // AutoMapper automatically maps CategoryName from Category.Name
                // AutoMapper automatically maps CategoryDescription from Category.Description
                
                // Custom mappings
                .ForMember(
                    dest => dest.PriceFormatted,
                    opt => opt.MapFrom<PriceFormatterResolver>())
                .ForMember(
                    dest => dest.InStock,
                    opt => opt.MapFrom(src => src.StockQuantity > 0))
                .ForMember(
                    dest => dest.AvailabilityStatus,
                    opt => opt.MapFrom<AvailabilityStatusResolver>());

            // ========================================
            // PROJECTION (For use with IQueryable/EF Core)
            // ========================================
            
            // ProjectTo is used with Entity Framework for efficient database queries
            // It translates the mapping to SQL and retrieves only necessary columns
            // Example usage: dbContext.Products.ProjectTo<ProductDTO>(_mapper.ConfigurationProvider)
        }
    }

    /// <summary>
    /// AutoMapper Profile: TypeConverterProfile
    /// Demonstrates custom type converters
    /// </summary>
    public class TypeConverterProfile : Profile
    {
        public TypeConverterProfile()
        {
            // Register custom type converters
            // Type converters work globally for all mappings of these types
            CreateMap<DateTime, string>().ConvertUsing<DateTimeToStringConverter>();
            CreateMap<string, DateTime>().ConvertUsing<StringToDateTimeConverter>();
            CreateMap<decimal, string>().ConvertUsing<DecimalToCurrencyConverter>();
            CreateMap<bool, string>().ConvertUsing<BoolToStatusConverter>();
        }
    }
}
