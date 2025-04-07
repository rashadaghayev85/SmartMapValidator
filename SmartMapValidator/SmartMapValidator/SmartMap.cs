using SmartMapValidator.Attributes;
using SmartMapValidator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SmartMapValidator
{
    public static class SmartMap
    {
        public static ValidationResult Validate<T>(T dto)
        {
            var errors = Validator.Validate(dto);
            var result = new ValidationResult();

            foreach (var error in errors)
            {
                result.AddError(error);
            }

            return result;
        }

        public static TDestination Map<TSource, TDestination>(TSource source)
            where TDestination : new()
        {
            var destination = new TDestination();

            var sourceProperties = typeof(TSource).GetProperties();
            var destinationProperties = typeof(TDestination).GetProperties();

            foreach (var sourceProperty in sourceProperties)
            {
                if (Attribute.IsDefined(sourceProperty, typeof(MapIgnoreAttribute)))
                    continue;

                var mapToAttribute = sourceProperty.GetCustomAttribute<MapToAttribute>();
                var targetName = mapToAttribute?.EntityField ?? sourceProperty.Name;

                var targetProperty = destinationProperties.FirstOrDefault(p => p.Name == targetName);
                if (targetProperty != null && targetProperty.CanWrite)
                {
                    var value = sourceProperty.GetValue(source);
                    targetProperty.SetValue(destination, value);
                }
            }

            return destination;
        }

        public static (TDestination, ValidationResult) MapAndValidate<TSource, TDestination>(TSource dto)
            where TDestination : new()
        {
            var validationResult = Validate(dto);
            if (!validationResult.IsValid)
                return (default, validationResult);

            var mapped = Map<TSource, TDestination>(dto);
            return (mapped, validationResult);
        }
    }
}
