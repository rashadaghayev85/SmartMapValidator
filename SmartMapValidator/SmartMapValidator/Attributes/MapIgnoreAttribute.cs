using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMapValidator.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class MapIgnoreAttribute : Attribute
    {
        // Empty class, as it is just a marker to indicate the property should not be mapped.
    }
}
