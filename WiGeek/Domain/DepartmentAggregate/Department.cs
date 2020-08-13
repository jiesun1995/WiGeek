using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WiGeek.Domain.SeedWork;
using WiGeek.Infrastructure.Attributes;

namespace WiGeek.Domain.DepartmentAggregate
{
    public class Department:HospitalAggregateRoot
    {
        //protected Department() { }
        //public Department(string hospitalCode,string hospitalId,string code,string name,string inputCode):this()
        //{
        //    HospitalId = hospitalId;
        //    HospitalCode = hospitalCode;
        //    Code = code;
        //    Name = name;
        //    InputCode = inputCode;
        //}
        [ColNumber(1)]
        [MaxLength(50)]
        public string Code { get; set; }
        [ColNumber(2)]
        [MaxLength(500)]
        public string Name { get; set; }
        [ColNumber(3)]
        [MaxLength(50)]
        public string InputCode { get; set; }
    }
}
