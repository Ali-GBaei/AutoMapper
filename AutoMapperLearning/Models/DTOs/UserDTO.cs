namespace AutoMapperLearning.Models.DTOs
{
    /// <summary>
    /// Data Transfer Object: UserDTO
    /// This is used to transfer data between layers (e.g., API to Client)
    /// DTOs help hide internal implementation and only expose necessary data
    /// </summary>
    public class UserDTO
    {
        public int Id { get; set; }
        
        // AutoMapper can automatically map properties with the same name
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        
        // Computed property - will be mapped using ForMember
        public string FullName { get; set; } = string.Empty;
        
        public string Email { get; set; } = string.Empty;
        
        // Different format for display
        public string DateOfBirthFormatted { get; set; } = string.Empty;
        
        // Calculated property
        public int Age { get; set; }
        
        // Nested DTO
        public AddressDTO? Address { get; set; }
        
        // Collection of DTOs
        public List<OrderDTO> Orders { get; set; } = new List<OrderDTO>();
        
        public int TotalOrders { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    /// <summary>
    /// DTO for Address - demonstrates nested mapping
    /// </summary>
    public class AddressDTO
    {
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        
        // Computed property combining multiple fields
        public string FullAddress { get; set; } = string.Empty;
    }

    /// <summary>
    /// DTO for Order - simplified version of domain model
    /// </summary>
    public class OrderDTO
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public string OrderDateFormatted { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public int ItemCount { get; set; }
    }

    /// <summary>
    /// DTO demonstrating flattening
    /// AutoMapper can automatically flatten nested properties
    /// </summary>
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PriceFormatted { get; set; } = string.Empty;
        
        // Flattened property - AutoMapper automatically maps Category.Name to CategoryName
        public string CategoryName { get; set; } = string.Empty;
        
        // Flattened property - AutoMapper automatically maps Category.Description to CategoryDescription
        public string CategoryDescription { get; set; } = string.Empty;
        
        public bool InStock { get; set; }
        public string AvailabilityStatus { get; set; } = string.Empty;
    }

    /// <summary>
    /// Simple DTO for demonstrating basic mapping
    /// </summary>
    public class UserBasicDTO
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    /// <summary>
    /// DTO for Employee
    /// </summary>
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
    }

    /// <summary>
    /// DTO for Manager (inherits from EmployeeDTO)
    /// Demonstrates mapping with inheritance
    /// </summary>
    public class ManagerDTO : EmployeeDTO
    {
        public int TeamSize { get; set; }
        public string OfficeLocation { get; set; } = string.Empty;
    }

    /// <summary>
    /// Create/Update DTO - different from read DTO
    /// Demonstrates reverse mapping
    /// </summary>
    public class CreateUserDTO
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
    }

    /// <summary>
    /// ViewModel for displaying user information in views
    /// </summary>
    public class UserViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Age { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string OrderSummary { get; set; } = string.Empty;
        public string AccountStatus { get; set; } = string.Empty;
    }
}
