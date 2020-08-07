using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WiGeek.Infrastructure.Attributes
{
    public class FileColNameAttribute: Attribute
    {
        public string Name { get; set; }
    }
}
