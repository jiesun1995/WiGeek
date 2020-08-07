using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using WiGeek.Domain.SeedWork;

namespace WiGeek.Domain.WardAggregate
{
    /// <summary>
    /// 病区字典
    /// </summary>
    public class Ward:HospitalAggregateRoot
    {
        protected Ward() { }
        public Ward(string hospitalCode, string hospitalId, string name, string inputCode)
        {
            HospitalId = hospitalId;
            HospitalCode = hospitalCode;
            Name = name;
            InputCode = inputCode;
        }

        /// <summary>
        /// 病区名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 过滤码
        /// </summary>
        public string InputCode { get; set; }
    }
}
