using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiGeek.Domain.SeedWork;

namespace WiGeek.Domain.ValueObject
{
    /// <summary>
    /// 医嘱类型
    /// </summary>
    public class OrderType : HospitalAggregateRoot
    {
        /// <summary>
        /// 医嘱类型名称
        /// </summary>
        public string Name { get; set; }
    }
}
