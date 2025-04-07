using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMapValidator.Interfaces
{
    public interface IMapDto<T>
    {
        T Map();
    }
}
