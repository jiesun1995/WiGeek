using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WiGeek.Infrastructure.Attributes
{
    public class ColNumberAttribute : Attribute
    {
        public ColNumberAttribute() { }
        public ColNumberAttribute(int colNumber)
        {
            ColNumber = colNumber;
        }
        public int ColNumber { get; set; }
    }
}
