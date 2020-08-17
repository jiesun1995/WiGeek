using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WiGeek.Infrastructure.Attributes
{
    public class ColNameAttribute:Attribute
    {
        public ColNameAttribute() { }
        public ColNameAttribute(string colName)
        {
            ColName = colName;
        }
        public string ColName { get; set; }
    }
}
