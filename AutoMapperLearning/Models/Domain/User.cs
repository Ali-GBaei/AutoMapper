namespace AutoMapperLearning.Models.Domain
{
    /// <summary>
    /// Domain Model: User
    /// This represents the entity in your database (Domain Layer)
    /// </summary>
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public Address? Address { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
    }

    /// <summary>
    /// Nested Domain Model: Address
    /// Demonstrates nested object mapping
    /// </summary>
    public class Address
    {
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }

    /// <summary>
    /// Domain Model: Order
    /// Demonstrates collection mapping
    /// </summary>
    public class Order
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    }

    /// <summary>
    /// Domain Model: OrderItem
    /// Demonstrates deeply nested collection mapping
    /// </summary>
    public class OrderItem
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    /// <summary>
    /// Domain Model: Product
    /// Demonstrates flattening and custom mappings
    /// </summary>
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public ProductCategory Category { get; set; } = new ProductCategory();
        public int StockQuantity { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    /// <summary>
    /// Domain Model: ProductCategory
    /// Used for flattening demonstration
    /// </summary>
    public class ProductCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    /// <summary>
    /// Domain Model: Employee (for inheritance demonstration)
    /// </summary>
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
    }

    /// <summary>
    /// Domain Model: Manager (inherits from Employee)
    /// Demonstrates mapping with inheritance
    /// </summary>
    public class Manager : Employee
    {
        public int TeamSize { get; set; }
        public string OfficeLocation { get; set; } = string.Empty;
    }
}
