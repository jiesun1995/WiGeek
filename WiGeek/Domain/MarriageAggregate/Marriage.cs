using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiGeek.Domain.SeedWork;
using WiGeek.Infrastructure.Attributes;

namespace WiGeek.Domain.MarriageAggregate
{
    /// <summary>
    /// 婚姻字典
    /// </summary>
    public class Marriage:HospitalAggregateRoot
    {
        public Marriage() { }
        public Marriage(string hospitalCode, string hospitalId, string code, string name)
        {
            HospitalId = hospitalId;
            HospitalCode = hospitalCode;
            Code = code;
            Name = name;
        }
        /// <summary>
        /// 婚姻编码
        /// </summary>
        [ColNumber(1)]
        public string Code { get; set; }
        /// <summary>
        /// 婚姻名称
        /// </summary>
        [ColNumber(2)]
        public string Name { get; set; }
    }
}
