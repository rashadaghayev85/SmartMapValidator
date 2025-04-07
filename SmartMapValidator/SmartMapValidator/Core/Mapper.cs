using SmartMapValidator.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SmartMapValidator.Core
{
    public static class Mapper
    {
        // DTO-nu Entity-yə çevirmək üçün əsas metod
        public static TDestination Map<TSource, TDestination>(TSource source)
            where TDestination : new()
        {
            // Yeni bir target obyekt yarat
            var destination = new TDestination();

            // Source və destination class-larını tapan properties-ləri al
            var sourceProperties = typeof(TSource).GetProperties().Where(p => p.CanRead);
            var destinationProperties = typeof(TDestination).GetProperties().Where(p => p.CanWrite);

            foreach (var sourceProperty in sourceProperties)
            {
                // MapIgnore atributu olanları atla
                if (Attribute.IsDefined(sourceProperty, typeof(MapIgnoreAttribute)))
                {
                    continue;
                }

                // MapTo atributu ilə hansı property-yə map ediləcəyini tap
                var mapToAttribute = sourceProperty.GetCustomAttribute<MapToAttribute>();
                string targetPropertyName = mapToAttribute?.EntityField ?? sourceProperty.Name;

                // Target property tapılarsa, dəyəri təyin et
                var targetProperty = destinationProperties.FirstOrDefault(p => p.Name == targetPropertyName);
                if (targetProperty != null)
                {
                    var value = sourceProperty.GetValue(source);
                    targetProperty.SetValue(destination, value);
                }
            }

            return destination;
        }
    }
}
