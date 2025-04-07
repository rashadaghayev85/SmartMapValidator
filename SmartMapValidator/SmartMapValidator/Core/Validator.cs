using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SmartMapValidator.Core
{
    public static class Validator
    {
        public static List<string> Validate<T>(T dto)
        {
            var validationErrors = new List<string>();

            // DTO sinifinin property-lərini əldə et
            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                // Required validation üçün nəzərdə tutulmuş xüsusi atributları tap
                var requiredAttribute = property.GetCustomAttribute<RequiredAttribute>();
                if (requiredAttribute != null)
                {
                    var value = property.GetValue(dto);
                    if (value == null || (value is string str && string.IsNullOrWhiteSpace(str)))
                    {
                        validationErrors.Add($"{property.Name} this field can't be empty ");
                    }
                }

                // Range validation
                var rangeAttribute = property.GetCustomAttribute<RangeAttribute>();
                if (rangeAttribute != null)
                {
                    var value = property.GetValue(dto);
                    if (value is IComparable)
                    {
                        var minValue = (IComparable)rangeAttribute.Minimum;
                        var maxValue = (IComparable)rangeAttribute.Maximum;

                        if (value is IComparable valueComparable)
                        {
                            if (valueComparable.CompareTo(minValue) < 0 || valueComparable.CompareTo(maxValue) > 0)
                            {
                                validationErrors.Add($"The {property.Name} field must be between {rangeAttribute.Minimum} and {rangeAttribute.Maximum}.");
                            }
                        }
                    }
                }

                // Digər validations (məsələn, regex validation, length validation və s.) əlavə oluna bilər
            }

            return validationErrors;
        }
    }

    // Custom Required validation atributu
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class RequiredAttribute : Attribute
    {
    }

    // Custom Range validation atributu
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class RangeAttribute : Attribute
    {
        public object Minimum { get; }
        public object Maximum { get; }

        public RangeAttribute(object minimum, object maximum)
        {
            Minimum = minimum;
            Maximum = maximum;
        }
    }
}
