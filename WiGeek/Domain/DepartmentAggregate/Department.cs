using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiGeek.Domain.SeedWork;

namespace WiGeek.Domain.DepartmentAggregate
{
    public class Department:HospitalAggregateRoot
    {        
        protected Department() { }
        public Department(string hospitalCode,string hospitalId,string code,string name,string inputCode):this()
        {
            HospitalId = hospitalId;
            HospitalCode = hospitalCode;
            Code = code;
            Name = name;
            InputCode = inputCode;
        }

        public string Code { get; set; }
        public string Name { get; set; }
        public string InputCode { get; set; }
    }
}
