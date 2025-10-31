using AutoMapper;

namespace AutoMapperLearning.CustomTypeConverters
{
    /// <summary>
    /// Custom Type Converter: Converts DateTime to formatted string
    /// ITypeConverter is used when you need to convert entire objects from one type to another
    /// 
    /// Use ITypeConverter when:
    /// - Converting between two completely different types
    /// - The conversion logic is complex and reusable
    /// - You want to convert the entire object, not just a property
    /// 
    /// Generic parameters:
    /// - TSource: Source type (DateTime)
    /// - TDestination: Destination type (string)
    /// </summary>
    public class DateTimeToStringConverter : ITypeConverter<DateTime, string>
    {
        public string Convert(DateTime source, string destination, ResolutionContext context)
        {
            // Format datetime to readable string
            return source.ToString("MMMM dd, yyyy");
        }
    }

    /// <summary>
    /// Custom Type Converter: Converts string to DateTime
    /// Demonstrates bidirectional conversion
    /// </summary>
    public class StringToDateTimeConverter : ITypeConverter<string, DateTime>
    {
        public DateTime Convert(string source, DateTime destination, ResolutionContext context)
        {
            // Try to parse the string to DateTime
            if (DateTime.TryParse(source, out var result))
            {
                return result;
            }
            
            // Return default if parsing fails
            return DateTime.MinValue;
        }
    }

    /// <summary>
    /// Custom Type Converter: Converts decimal to currency string
    /// </summary>
    public class DecimalToCurrencyConverter : ITypeConverter<decimal, string>
    {
        public string Convert(decimal source, string destination, ResolutionContext context)
        {
            return $"${source:N2}";
        }
    }

    /// <summary>
    /// Custom Type Converter: Converts boolean to status string
    /// </summary>
    public class BoolToStatusConverter : ITypeConverter<bool, string>
    {
        public string Convert(bool source, string destination, ResolutionContext context)
        {
            return source ? "Active" : "Inactive";
        }
    }

    /// <summary>
    /// Custom Type Converter: Example of complex object conversion
    /// Converts a list of strings to a comma-separated string
    /// </summary>
    public class ListToCommaSeparatedStringConverter : ITypeConverter<List<string>, string>
    {
        public string Convert(List<string> source, string destination, ResolutionContext context)
        {
            if (source == null || !source.Any())
                return string.Empty;

            return string.Join(", ", source);
        }
    }
}
