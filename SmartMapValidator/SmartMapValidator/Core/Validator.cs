using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SmartMapValidator.Core
{
    public static class Validator
    {
        public static List<string> Validate<T>(T dto)
        {
            var validationErrors = new List<string>();
            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                var value = property.GetValue(dto);

                // Required
                var requiredAttribute = property.GetCustomAttribute<RequiredAttribute>();
                if (requiredAttribute != null)
                {
                    if (value == null || (value is string str && string.IsNullOrWhiteSpace(str)))
                        validationErrors.Add($"{property.Name} this field can't be empty");
                }

                // Range
                var rangeAttribute = property.GetCustomAttribute<RangeAttribute>();
                if (rangeAttribute != null && value is IComparable comparable)
                {
                    if (comparable.CompareTo((IComparable)rangeAttribute.Minimum) < 0 ||
                        comparable.CompareTo((IComparable)rangeAttribute.Maximum) > 0)
                        validationErrors.Add($"The {property.Name} field must be between {rangeAttribute.Minimum} and {rangeAttribute.Maximum}.");
                }

                // MaxLength
                var maxLengthAttribute = property.GetCustomAttribute<MaxLengthAttribute>();
                if (maxLengthAttribute != null && value is string strVal && strVal.Length > maxLengthAttribute.Length)
                    validationErrors.Add($"{property.Name} cannot exceed {maxLengthAttribute.Length} characters.");

                // MinLength
                var minLengthAttribute = property.GetCustomAttribute<MinLengthAttribute>();
                if (minLengthAttribute != null && value is string strMin && strMin.Length < minLengthAttribute.Length)
                    validationErrors.Add($"{property.Name} must have at least {minLengthAttribute.Length} characters.");

                // Nullable
                var nullableAttribute = property.GetCustomAttribute<NullableAttribute>();
                if (nullableAttribute != null && value == null)
                    validationErrors.Add($"{property.Name} can be null."); // Bu əslində error deyil, info xarakterlidir, istəsən çıxara bilərsən.

                // Regex
                var regexAttribute = property.GetCustomAttribute<RegexAttribute>();
                if (regexAttribute != null && value is string regexStr && !Regex.IsMatch(regexStr, regexAttribute.Pattern))
                    validationErrors.Add($"{property.Name} is not in the correct format.");
            }

            return validationErrors;
        }
    }

    // Atributlar eyni qalır
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class RequiredAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Property)]
    public sealed class RangeAttribute : Attribute
    {
        public object Minimum { get; }
        public object Maximum { get; }

        public RangeAttribute(object min, object max)
        {
            Minimum = min;
            Maximum = max;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public sealed class MaxLengthAttribute : Attribute
    {
        public int Length { get; }

        public MaxLengthAttribute(int length) => Length = length;
    }

    [AttributeUsage(AttributeTargets.Property)]
    public sealed class MinLengthAttribute : Attribute
    {
        public int Length { get; }

        public MinLengthAttribute(int length) => Length = length;
    }

    [AttributeUsage(AttributeTargets.Property)]
    public sealed class RegexAttribute : Attribute
    {
        public string Pattern { get; }

        public RegexAttribute(string pattern) => Pattern = pattern;
    }

    [AttributeUsage(AttributeTargets.Property)]
    public sealed class NullableAttribute : Attribute { }
}
