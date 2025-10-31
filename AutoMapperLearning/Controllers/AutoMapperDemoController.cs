using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using AutoMapperLearning.Models.Domain;
using AutoMapperLearning.Models.DTOs;

namespace AutoMapperLearning.Controllers
{
    /// <summary>
    /// AutoMapperDemoController
    /// 
    /// This controller demonstrates various AutoMapper features and use cases.
    /// It includes examples for beginners to advanced users.
    /// 
    /// AutoMapper is a convention-based object-object mapper that eliminates
    /// the need for tedious manual mapping code.
    /// </summary>
    public class AutoMapperDemoController : Controller
    {
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor with Dependency Injection
        /// IMapper is injected through ASP.NET Core's built-in DI container
        /// </summary>
        public AutoMapperDemoController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Home page - Overview of AutoMapper features
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }

        // ========================================
        // BEGINNER EXAMPLES
        // ========================================

        /// <summary>
        /// Example 1: Basic Mapping
        /// Demonstrates the simplest form of AutoMapper usage
        /// Maps an object with properties of the same name
        /// </summary>
        public IActionResult BasicMapping()
        {
            // Create a source object
            var employee = new Employee
            {
                Id = 1,
                Name = "John Doe",
                Email = "john.doe@example.com",
                Department = "IT"
            };

            // Map to DTO using AutoMapper
            // Syntax: _mapper.Map<DestinationType>(sourceObject)
            var employeeDTO = _mapper.Map<EmployeeDTO>(employee);

            ViewBag.Title = "Basic Mapping Example";
            ViewBag.Description = "AutoMapper automatically maps properties with the same name and compatible types.";
            
            return View("MappingResult", employeeDTO);
        }

        /// <summary>
        /// Example 2: Reverse Mapping
        /// Demonstrates mapping in the opposite direction
        /// Useful when creating/updating entities from DTOs
        /// </summary>
        public IActionResult ReverseMapping()
        {
            // Create a DTO (from user input, API call, etc.)
            var createUserDTO = new CreateUserDTO
            {
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@example.com",
                DateOfBirth = new DateTime(1990, 5, 15)
            };

            // Map DTO to domain model
            var user = _mapper.Map<User>(createUserDTO);
            
            // Set additional properties not in DTO
            user.Id = 1;
            user.CreatedAt = DateTime.Now;
            user.IsActive = true;

            ViewBag.Title = "Reverse Mapping Example";
            ViewBag.Description = "Map from DTO back to domain model (useful for Create/Update operations).";
            
            return View("UserResult", user);
        }

        /// <summary>
        /// Example 3: Collection Mapping
        /// Demonstrates mapping lists/collections of objects
        /// </summary>
        public IActionResult CollectionMapping()
        {
            // Create a list of employees
            var employees = new List<Employee>
            {
                new Employee { Id = 1, Name = "John Doe", Email = "john@example.com", Department = "IT" },
                new Employee { Id = 2, Name = "Jane Smith", Email = "jane@example.com", Department = "HR" },
                new Employee { Id = 3, Name = "Bob Johnson", Email = "bob@example.com", Department = "Sales" }
            };

            // Map collection to collection
            // AutoMapper handles collections automatically
            var employeeDTOs = _mapper.Map<List<EmployeeDTO>>(employees);

            ViewBag.Title = "Collection Mapping Example";
            ViewBag.Description = "AutoMapper can map entire collections in one call.";
            
            return View("EmployeeList", employeeDTOs);
        }

        // ========================================
        // INTERMEDIATE EXAMPLES
        // ========================================

        /// <summary>
        /// Example 4: Custom Property Mapping
        /// Demonstrates ForMember with custom logic
        /// </summary>
        public IActionResult CustomPropertyMapping()
        {
            var user = GetSampleUser();

            // Map with custom property mappings defined in Profile
            var userDTO = _mapper.Map<UserDTO>(user);

            ViewBag.Title = "Custom Property Mapping";
            ViewBag.Description = "Use ForMember() to map properties with custom logic (e.g., FullName = FirstName + LastName).";
            
            return View("UserDTOResult", userDTO);
        }

        /// <summary>
        /// Example 5: Nested Object Mapping
        /// Demonstrates mapping objects with nested properties
        /// </summary>
        public IActionResult NestedObjectMapping()
        {
            var user = GetSampleUser();
            user.Address = new Address
            {
                Street = "123 Main St",
                City = "New York",
                State = "NY",
                ZipCode = "10001",
                Country = "USA"
            };

            // AutoMapper automatically maps nested objects
            var userDTO = _mapper.Map<UserDTO>(user);

            ViewBag.Title = "Nested Object Mapping";
            ViewBag.Description = "AutoMapper recursively maps nested objects if mappings are defined.";
            
            return View("UserDTOResult", userDTO);
        }

        /// <summary>
        /// Example 6: Flattening
        /// Demonstrates AutoMapper's automatic flattening capability
        /// </summary>
        public IActionResult Flattening()
        {
            var product = new Product
            {
                Id = 1,
                Name = "Laptop",
                Description = "High-performance laptop",
                Price = 1299.99m,
                StockQuantity = 25,
                CreatedDate = DateTime.Now.AddMonths(-2),
                Category = new ProductCategory
                {
                    Id = 1,
                    Name = "Electronics",
                    Description = "Electronic devices and accessories"
                }
            };

            // AutoMapper automatically flattens Category.Name to CategoryName
            var productDTO = _mapper.Map<ProductDTO>(product);

            ViewBag.Title = "Flattening Example";
            ViewBag.Description = "AutoMapper automatically flattens nested properties (e.g., Category.Name -> CategoryName).";
            
            return View("ProductResult", productDTO);
        }

        // ========================================
        // ADVANCED EXAMPLES
        // ========================================

        /// <summary>
        /// Example 7: Custom Value Resolvers
        /// Demonstrates using custom resolvers for complex mapping logic
        /// </summary>
        public IActionResult CustomValueResolvers()
        {
            var user = GetSampleUser();
            user.DateOfBirth = new DateTime(1985, 3, 15);
            user.Orders = new List<Order>
            {
                new Order { Id = 1, OrderNumber = "ORD001", OrderDate = DateTime.Now.AddDays(-10), TotalAmount = 150.00m, Status = "Delivered" },
                new Order { Id = 2, OrderNumber = "ORD002", OrderDate = DateTime.Now.AddDays(-5), TotalAmount = 75.50m, Status = "Shipped" }
            };

            // Mapping uses custom resolvers defined in Profile
            // - AgeResolver: Calculates age from DateOfBirth
            // - UserStatusResolver: Determines status based on activity
            var userDTO = _mapper.Map<UserDTO>(user);

            ViewBag.Title = "Custom Value Resolvers";
            ViewBag.Description = "Use IValueResolver for complex property mapping logic that requires context or calculations.";
            
            return View("UserDTOResult", userDTO);
        }

        /// <summary>
        /// Example 8: Conditional Mapping
        /// Demonstrates mapping properties based on conditions
        /// </summary>
        public IActionResult ConditionalMapping()
        {
            var activeUser = GetSampleUser();
            activeUser.IsActive = true;

            var inactiveUser = GetSampleUser();
            inactiveUser.IsActive = false;
            inactiveUser.Email = "inactive@example.com";

            // Map with conditional logic (Email only mapped if user is active)
            var activeUserDTO = _mapper.Map<UserBasicDTO>(activeUser);
            var inactiveUserDTO = _mapper.Map<UserBasicDTO>(inactiveUser);

            ViewBag.Title = "Conditional Mapping";
            ViewBag.Description = "Use Condition() to map properties only when certain conditions are met.";
            ViewBag.ActiveUser = activeUserDTO;
            ViewBag.InactiveUser = inactiveUserDTO;
            
            return View("ConditionalResult");
        }

        /// <summary>
        /// Example 9: Inheritance Mapping
        /// Demonstrates mapping with class inheritance
        /// </summary>
        public IActionResult InheritanceMapping()
        {
            var manager = new Manager
            {
                Id = 1,
                Name = "Sarah Johnson",
                Email = "sarah.johnson@example.com",
                Department = "Engineering",
                TeamSize = 12,
                OfficeLocation = "Building A, Floor 3"
            };

            // Map Manager to ManagerDTO (includes base Employee properties)
            var managerDTO = _mapper.Map<ManagerDTO>(manager);

            ViewBag.Title = "Inheritance Mapping";
            ViewBag.Description = "Use IncludeBase() to map inherited properties from base classes.";
            
            return View("ManagerResult", managerDTO);
        }

        /// <summary>
        /// Example 10: Projection (for use with Entity Framework)
        /// Demonstrates how AutoMapper works with IQueryable for efficient database queries
        /// </summary>
        public IActionResult ProjectionExample()
        {
            // In a real application with Entity Framework:
            // var users = dbContext.Users.ProjectTo<UserDTO>(_mapper.ConfigurationProvider).ToList();
            
            // ProjectTo translates the mapping to SQL, so only necessary columns are retrieved
            // This is much more efficient than:
            // var users = dbContext.Users.ToList(); // Retrieves all columns
            // var userDTOs = _mapper.Map<List<UserDTO>>(users); // Then maps in memory

            ViewBag.Title = "Projection (EF Core)";
            ViewBag.Description = "Use ProjectTo() with Entity Framework to translate mappings to SQL for better performance.";
            ViewBag.Code = @"
                // Instead of this (inefficient):
                var users = dbContext.Users.ToList();
                var userDTOs = _mapper.Map<List<UserDTO>>(users);

                // Do this (efficient):
                var userDTOs = dbContext.Users
                    .ProjectTo<UserDTO>(_mapper.ConfigurationProvider)
                    .ToList();
            ";
            
            return View("CodeExample");
        }

        /// <summary>
        /// Example 11: UpdateExistingObject
        /// Demonstrates mapping to update an existing object instead of creating a new one
        /// </summary>
        public IActionResult UpdateExistingObject()
        {
            // Existing entity from database
            var existingUser = GetSampleUser();
            existingUser.Id = 5;
            existingUser.CreatedAt = DateTime.Now.AddYears(-1);

            // Updated data from user input
            var updateDTO = new CreateUserDTO
            {
                FirstName = "John Updated",
                LastName = "Doe Updated",
                Email = "john.updated@example.com",
                DateOfBirth = new DateTime(1988, 8, 20)
            };

            // Map to existing object (updates properties instead of creating new object)
            // Syntax: _mapper.Map(source, destination)
            _mapper.Map(updateDTO, existingUser);

            ViewBag.Title = "Update Existing Object";
            ViewBag.Description = "Use _mapper.Map(source, destination) to update an existing object instead of creating a new one.";
            ViewBag.Note = "Notice the Id and CreatedAt are preserved from the existing user.";
            
            return View("UserResult", existingUser);
        }

        // ========================================
        // HELPER METHODS
        // ========================================

        /// <summary>
        /// Helper method to create a sample user
        /// </summary>
        private User GetSampleUser()
        {
            return new User
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                DateOfBirth = new DateTime(1990, 1, 1),
                CreatedAt = DateTime.Now.AddMonths(-6),
                IsActive = true,
                Orders = new List<Order>()
            };
        }

        /// <summary>
        /// Summary page showing all AutoMapper features
        /// </summary>
        public IActionResult Features()
        {
            return View();
        }

        /// <summary>
        /// Advantages of AutoMapper
        /// </summary>
        public IActionResult Advantages()
        {
            return View();
        }

        /// <summary>
        /// Best Practices
        /// </summary>
        public IActionResult BestPractices()
        {
            return View();
        }
    }
}
