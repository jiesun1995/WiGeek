using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiGeek.Domain.SeedWork;
using WiGeek.Infrastructure.Attributes;

namespace WiGeek.Domain.WorkAggregate
{
    /// <summary>
    /// 工作字典
    /// </summary>
    public class Work:HospitalAggregateRoot
    {
        //protected Work() { }
        //public Work(string hospitalCode, string hospitalId, string name, string code)
        //{
        //    HospitalId = hospitalId;
        //    HospitalCode = hospitalCode;
        //    Name = name;
        //    Code = code;
        //}
        /// <summary>
        /// 工作编码
        /// </summary>
        [ColNumber(1)]
        public string Code { get; set; }
        /// <summary>
        /// 工作名称
        /// </summary>
        [ColNumber(2)]
        public string Name { get; set; }

    }
}
