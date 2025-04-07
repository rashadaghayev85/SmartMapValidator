using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMapValidator.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class MapToAttribute : Attribute
    {
        public string EntityField { get; }

        // Constructor to initialize the attribute with the target entity field
        public MapToAttribute(string entityField)
        {
            EntityField = entityField;
        }
    }
}
