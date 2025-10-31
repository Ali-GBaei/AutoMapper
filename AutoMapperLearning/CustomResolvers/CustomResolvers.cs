using AutoMapper;
using AutoMapperLearning.Models.Domain;
using AutoMapperLearning.Models.DTOs;

namespace AutoMapperLearning.CustomResolvers
{
    /// <summary>
    /// Custom Value Resolver: Calculates age from date of birth
    /// IValueResolver is used when you need custom logic to resolve a single property
    /// 
    /// Generic parameters:
    /// - TSource: Source type (User)
    /// - TDestination: Destination type (UserDTO)
    /// - TDestMember: Type of the destination member (int for Age)
    /// </summary>
    public class AgeResolver : IValueResolver<User, UserDTO, int>
    {
        public int Resolve(User source, UserDTO destination, int destMember, ResolutionContext context)
        {
            // Custom logic to calculate age
            var today = DateTime.Today;
            var age = today.Year - source.DateOfBirth.Year;
            
            // Adjust if birthday hasn't occurred this year
            if (source.DateOfBirth.Date > today.AddYears(-age))
            {
                age--;
            }
            
            return age;
        }
    }

    /// <summary>
    /// Custom Value Resolver: Formats full address from address object
    /// Demonstrates working with nested objects
    /// </summary>
    public class FullAddressResolver : IValueResolver<Address, AddressDTO, string>
    {
        public string Resolve(Address source, AddressDTO destination, string destMember, ResolutionContext context)
        {
            if (source == null)
                return string.Empty;

            // Custom formatting logic
            return $"{source.Street}, {source.City}, {source.State} {source.ZipCode}, {source.Country}";
        }
    }

    /// <summary>
    /// Custom Value Resolver: Determines user status based on activity
    /// Demonstrates conditional logic in resolvers
    /// </summary>
    public class UserStatusResolver : IValueResolver<User, UserDTO, string>
    {
        public string Resolve(User source, UserDTO destination, string destMember, ResolutionContext context)
        {
            if (!source.IsActive)
                return "Inactive";

            var daysSinceCreation = (DateTime.Now - source.CreatedAt).Days;
            
            if (daysSinceCreation < 30)
                return "New User";
            else if (source.Orders.Count > 10)
                return "Premium User";
            else if (source.Orders.Count > 0)
                return "Active User";
            else
                return "Registered User";
        }
    }

    /// <summary>
    /// Custom Value Resolver: Formats price with currency symbol
    /// Demonstrates formatting in resolvers
    /// </summary>
    public class PriceFormatterResolver : IValueResolver<Product, ProductDTO, string>
    {
        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            return $"${source.Price:N2}";
        }
    }

    /// <summary>
    /// Custom Value Resolver: Determines product availability
    /// Demonstrates business logic in resolvers
    /// </summary>
    public class AvailabilityStatusResolver : IValueResolver<Product, ProductDTO, string>
    {
        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            if (source.StockQuantity == 0)
                return "Out of Stock";
            else if (source.StockQuantity < 10)
                return "Low Stock";
            else if (source.StockQuantity < 50)
                return "In Stock";
            else
                return "Plenty Available";
        }
    }

    /// <summary>
    /// IMemberValueResolver: Alternative to IValueResolver
    /// Used when you only need the source object, not the entire destination
    /// 
    /// This is more efficient when you don't need the destination object
    /// </summary>
    public class OrderSummaryResolver : IMemberValueResolver<User, UserViewModel, List<Order>, string>
    {
        public string Resolve(User source, UserViewModel destination, List<Order> sourceMember, string destMember, ResolutionContext context)
        {
            if (sourceMember == null || !sourceMember.Any())
                return "No orders yet";

            var totalOrders = sourceMember.Count;
            var totalSpent = sourceMember.Sum(o => o.TotalAmount);
            
            return $"{totalOrders} orders - Total: ${totalSpent:N2}";
        }
    }
}
